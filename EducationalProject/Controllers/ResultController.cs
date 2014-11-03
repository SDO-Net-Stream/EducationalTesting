using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EducationalProject.DataInfo;
using EducationalProject.Models;
using WebMatrix.WebData;

namespace EducationalProject.Controllers
{
    public class ResultController : Controller
    {
        [Authorize(Roles = "User")]
        public ActionResult Results()
        {
            var resultsWrapper = new List<ResultsWrapper>();
            using (var db = new UsersContext())
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var resultList = db.TestsResults.Where(result=>result.User.UserId==userId)
                    .OrderByDescending(date => date.DatePassing).ToList();

                resultsWrapper.AddRange(resultList.Select(result => new ResultsWrapper
                {
                    TestResultId = result.TestResultId,
                    TestName = result.Test.TestName,
                    DatePassing = result.DatePassing,
                    Passed = result.Passed,
                    PercentTaken = result.PercentTaken
                }));
            }
            return View(resultsWrapper);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult DetailsResult(int testResultId)
        {
            var detailsWrapper = new DetailsResultWrapper();
            using (var db = new UsersContext())
            {
                var userId = WebSecurity.GetUserId(User.Identity.Name);
                var testResult =
                    db.TestsResults.FirstOrDefault(
                        result => result.User.UserId == userId && result.TestResultId == testResultId);
                if (testResult != null)
                {
                    detailsWrapper.TestName = testResult.Test.TestName;
                    detailsWrapper.PercentTaken = testResult.PercentTaken;
                    detailsWrapper.Passed = testResult.Passed;
                    
                    var actionId = testResult.Action.ActionId;
                    var historyList = db.TestHistory.Where(h => h.Action.ActionId == actionId).ToList();
                    detailsWrapper.QuestionsList = GetQuestionDetailList(historyList, testResult);
                }
            }
            return View(detailsWrapper);
        }

        private List<QuestionDetails> GetQuestionDetailList(IList<TestHistory> historyList, TestResults testResult)
        {
            var questionsList= new List<QuestionDetails>();
            foreach (var testHistory in historyList)
            {
                var question = testResult.Test.Questions.FirstOrDefault(n => n.Number == testHistory.Number);
                if (question != null)
                {
                    if (question.TestType == DataConst.CheckedType || question.TestType == DataConst.RadioType)
                    {
                        var questionDetails = new CheckedQuestion
                        {
                            Type = question.TestType,
                            QuestionText = question.Text,
                            UserNumber = DataConst.UserCheckNumber + historyList.IndexOf(testHistory),
                            RealNumber = DataConst.RealCheckNumber + historyList.IndexOf(testHistory),
                            VariantsList = new List<CheckedVariant>()
                        };
                        var listVariant = ((QuestionWithVariants)question).VariantAnswers.ToList();
                        foreach (var variantAnswer in listVariant)
                        {
                            var variant = new CheckedVariant { Text = variantAnswer.Text };
                            var index = listVariant.IndexOf(variantAnswer);
                            variant.RealAnswer = question.Answer[index] == '1';
                            variant.UserAnswer = false;
                            if (testHistory.UserAnswer != null)
                            {
                                if (testHistory.UserAnswer.Count() >= index)
                                {
                                    variant.UserAnswer = testHistory.UserAnswer[index] == '1';
                                }
                            }
                            questionDetails.VariantsList.Add(variant);
                        }
                        questionsList.Add(questionDetails);
                    }
                }
            }
            return questionsList;
        }
	}
}