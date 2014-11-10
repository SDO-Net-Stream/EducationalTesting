using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationalProject.DataInfo;
using EducationalProject.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using WebMatrix.WebData;
using Action = EducationalProject.Models.Action;

namespace EducationalProject.Controllers
{
    public class TestController : Controller
    {
        [Authorize(Roles = "User")]
        public ActionResult Tests()
        {
            var testWrappers = new List<TestInfoWrapper>();
            using (var db = new UsersContext())
            {
                var testInfoList =
                    db.Tests.OrderByDescending(date => date.DateDownload)
                        .ToList();

                testWrappers.AddRange(testInfoList.Select(test => new TestInfoWrapper
                {
                    TestId = test.TestId,
                    TestName = test.TestName,
                    CountQuestions = test.Questions.Count
                }));
            }
            return View(testWrappers);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult StartTest(int id)
        {
            using (var db = new UsersContext())
            {
                var test = db.Tests.FirstOrDefault(t => t.TestId == id);
                if (test == null) {return RedirectToAction("RolePermissions", "Home");}
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var actionInProgress = db.Actions.FirstOrDefault(a => a.User.UserId == userId && a.Status == 1);
                if (actionInProgress != null || test.Questions.Count == 0)
                {
                    return RedirectToAction("Tests", "Test");
                }
                var action=new Action
                {
                    User = db.UserProfiles.FirstOrDefault(u => u.UserId == userId),
                    Test = test,
                    Status = 1,   
                    TestsInProgres = new List<TestInProgres>()
                };
                var listQuestion = test.Questions.OrderBy(q => q.Number).ToList();
                foreach (var question in listQuestion)
                {
                    action.TestsInProgres.Add(new TestInProgres
                    {
                        Action = action,
                        Question = question,
                        Submitted = false
                    });
                }
                action.DateStart = DateTime.Now;
                db.Actions.Add(action);
                db.SaveChanges();
            }
            return RedirectToAction("TestInAction", "Test", new {number = 1});
        }

        [Authorize(Roles = "User")]
        public ActionResult TestInAction(int number)
        {
            using (var db = new UsersContext())
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var action = db.Actions.FirstOrDefault(a => a.User.UserId == userId && a.Status == 1);
                if (action != null)
                {
                    var currentQeustion =
                        action.TestsInProgres.FirstOrDefault(q => q.Question.Number==number);
                    if (currentQeustion != null)
                    {
                        if (currentQeustion.Question is QuestionWithVariants)
                        {
                            var questionInProgres = new QuestionInProgresWrapper(number, currentQeustion, action);
                            return View(questionInProgres);
                        }
                    }
                }
            }
            ViewBag.WarningMessage = DataConst.WarningMessageForTest;
            return RedirectToAction("Tests", "Test");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult GoToQuestion(QuestionInProgresWrapper result, string submit)
        {
            using (var db = new UsersContext())
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var action = db.Actions.FirstOrDefault(a => a.User.UserId == userId && a.Status == 1);
                var response = new ResponseAction(submit);
                if (action != null && response.CurrentNumber != null)
                {
                    var currentQuestion =
                        action.TestsInProgres.FirstOrDefault(q => q.Question.Number == response.CurrentNumber);
                    if (currentQuestion != null)
                    {
                       return CheckCurrentQuestion(result, response, userId, action, currentQuestion, db);
                    }
                }
            }
            ViewBag.WarningMessage = DataConst.WarningMessageForTest;
            return RedirectToAction("TestInAction", "Test", new { number = 1 });
        }

        private ActionResult CheckCurrentQuestion(QuestionInProgresWrapper result, ResponseAction response, int userId,
            Action action, TestInProgres currentQuestion, UsersContext db)
        {
            try
            {
                if (currentQuestion.Question.TestType == DataConst.CheckedType)
                {
                    SaveResultOfCheckedQuestion(result, currentQuestion, db);
                }
                if (currentQuestion.Question.TestType == DataConst.RadioType)
                {
                    SaveResultOfRadioQuestion(result, currentQuestion, db);
                }
               return ExecuteCommand(response, userId, action, currentQuestion, db);
            }
            catch (Exception)
            {
                ViewBag.WarningMessage = DataConst.WarningErrorMessage;
                return RedirectToAction("TestInAction", "Test", new { number = 1 });
            }
        }

        private ActionResult ExecuteCommand(ResponseAction response, int userId,
            Action action, TestInProgres currentQuestion, UsersContext db
            )
        {
            switch (response.Command)
            {
                case DataConst.Submit:
                {
                    SubmitTest(userId, currentQuestion, action, db);
                    return RedirectToAction("Results", "Result");
                }
                case DataConst.Next:
                {
                    if (response.CurrentNumber.HasValue)
                    {
                        return RedirectToAction("TestInAction", "Test", new { number = response.CurrentNumber + 1});
                    }
                    break;
                }
                case DataConst.Prev:
                {
                    if (response.CurrentNumber.HasValue)
                    {
                        return RedirectToAction("TestInAction", "Test", new { number = response.CurrentNumber - 1 });
                    }
                    break;
                }
                case DataConst.GoTo:
                {
                    if (response.GoTo.HasValue)
                    {
                        return RedirectToAction("TestInAction", "Test", new { number = response.GoTo });
                    }
                    break;
                }
            }
            ViewBag.WarningMessage = DataConst.WarningErrorMessage;
            return RedirectToAction("TestInAction", "Test", new { number = 1 });
        }

        private void SubmitTest(int userId, TestInProgres currentQuestion, Action action, UsersContext db)
        {
                currentQuestion.Submitted = true;
                action.DatePassing = DateTime.Now;
                action.Status = 2;
                db.SaveChanges();
                CalculateResult(userId, action, db);
                CopyToHistory(action, db);
                RemoveFromProgres(action, db);
        }


        private void CalculateResult(int userId, Action action, UsersContext db)
        {
            var testResult = new TestResults
            {
                User = db.UserProfiles.FirstOrDefault(u => u.UserId == userId),
                Action = action,
                Test = action.Test,
                DatePassing = action.DatePassing
            };
            double pointSum = 0;
            foreach (var test in action.TestsInProgres)
            {
                if (test.UserAnswer != null)
                {
                    switch (test.Question.TestType)
                    {
                        case DataConst.CheckedType:
                        {
                            pointSum += (CalculateChecked(test));
                            break;
                        }
                        case DataConst.RadioType:
                        {
                            pointSum += (CalculateRadio(test));
                            break;
                        }
                    }
                }
            }
            testResult.PercentTaken = Convert.ToInt32((pointSum/action.TestsInProgres.Count)*100);
            testResult.Passed = testResult.PercentTaken >= DataConst.PercentLimit;
            db.TestsResults.Add(testResult);
            db.SaveChanges();
        }

        private void CopyToHistory(Action action, UsersContext db)
        {
            foreach (var test in action.TestsInProgres)
            {
                var testToHistory = new TestHistory
                {
                    Action = action,
                    Number = test.Question.Number,
                    Question = test.Question,
                    UserAnswer = test.UserAnswer
                };
                db.TestHistory.Add(testToHistory);
                db.SaveChanges();
            }
        }

        private void RemoveFromProgres(Action action, UsersContext db)
        {
            db.Actions.Attach(action);

            var testsList= action.TestsInProgres.ToList();

            foreach (var testInProgres in testsList)
            {
                action.TestsInProgres.Remove(testInProgres);
                db.TestsInProgres.Remove(testInProgres);
            }
            db.SaveChanges(); 
        }

        private void SaveResultOfCheckedQuestion(QuestionInProgresWrapper result, TestInProgres currentQuestion, UsersContext db)
        {
            var userAnswer = result.AnswerVariantList.Aggregate("",
                                (current, res) => current + (res.Selected ? "1" : "0"));
            currentQuestion.UserAnswer = userAnswer;
            currentQuestion.Submitted = true;
            db.SaveChanges();
        }

        private void SaveResultOfRadioQuestion(QuestionInProgresWrapper result, TestInProgres currentQuestion, UsersContext db)
        {
            var userAnswer = "";

            for (var index = 0; index < ((QuestionWithVariants)currentQuestion.Question).VariantAnswers.Count; index++)
            {
                int number;
                if (Int32.TryParse(result.SelectedAnswer, out number))
                {
                    userAnswer += (number == index) ? "1" : "0";
                }
                else
                {
                    userAnswer += "0";
                }
            }
            currentQuestion.UserAnswer = userAnswer;
            currentQuestion.Submitted = true;
            db.SaveChanges();
        }

        private static double CalculateChecked(TestInProgres test)
        {
            if (test.UserAnswer.Count() != test.Question.Answer.Count())
            {
                return 0;
            }
            var answerPoints = 0;
            var userPoints = 0;
            if (test.UserAnswer != null)
            {
                for (int index = 0; index < test.Question.Answer.Count(); ++index)
                {
                    if (test.Question.Answer[index] == '1')
                    {
                        ++answerPoints;
                        switch (test.UserAnswer[index])
                        {
                            case '1':
                                ++userPoints;
                                break;
                            default:
                                --userPoints;
                                break;
                        }
                    }
                    else
                    {
                        switch (test.UserAnswer[index])
                        {
                            case '1':
                                --userPoints;
                                break;
                        }
                    }
                }
            }
            return userPoints > 0 ? userPoints/(double) answerPoints : 0;
        }

        private static double CalculateRadio(TestInProgres test)
        {
            if (test.UserAnswer != null)
            {
                return test.Question.Answer == test.UserAnswer ? 1 : 0;
            }
            return 0;
        }
    }
}