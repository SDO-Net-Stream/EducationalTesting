using EduTesting.Interfaces;
using EduTesting.Model;
using EduTesting.ViewModels.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class ExamService : IExamService
    {
        private readonly IEduTestingRepository _Repository;
        private readonly IWebUserManager _webUser;
        public ExamService(IEduTestingRepository repository, IWebUserManager webUser)
        {
            _Repository = repository;
            _webUser = webUser;
        }

        public void StartTest(StartTestViewModel startModel)
        {
            var user = _webUser.CurrentUser;
            var groups = user.UserGroups.Select(g => g.GroupID).ToArray();
            var exam = _Repository.GetActiveTestResultByUser(startModel.TestId, _webUser.CurrentUser.UserId);
            if (exam != null)
                throw new BusinessLogicException("Test already started. Complete previous before starting new one.");
            var test = _Repository.SelectById<Test>(startModel.TestId);
            if (
                test.TestStatus != TestStatus.Published ||
                !(
                    test.TestAttributes.Any(a => a.AttributeId == (int)AttributeCode.TestIsPublic) ||
                    test.UserGroups.Any(g => groups.Contains(g.GroupID))
                )
            )
            {
                throw new BusinessLogicException("You can not start this test");
            }
            var attributes = test.TestAttributes.ToDictionary(a => (AttributeCode)a.AttributeId, a => a.AttributeValue);
            exam = new TestResult
            {
                UserId = _webUser.CurrentUser.UserId,
                TestId = test.TestId,
                TestResultBeginTime = DateTime.UtcNow,
                TestResultStatus = TestResultStatus.InProgress,
                TestResultEndTime = null
            };
            if (attributes.ContainsKey(AttributeCode.TestTimeLimit))
            {
                exam.TestResultEndTime = exam.TestResultBeginTime.AddMinutes(int.Parse(attributes[AttributeCode.TestTimeLimit]));
            }
            exam = _Repository.Insert<TestResult>(exam, false);
            var questions = test.Questions.ToList();
            if (attributes.ContainsKey(AttributeCode.TestRandomSubsetSize))
            {
                var maxQuestions = Math.Max(0, int.Parse(attributes[AttributeCode.TestRandomSubsetSize]));
                var random = new Random((int)DateTime.Now.Ticks);
                while (questions.Count > maxQuestions)
                    questions.RemoveAt(random.Next(questions.Count));

            }
            foreach (var question in questions)
            {
                _Repository.Insert(new UserAnswer
                {
                    TestResultId = exam.TestResultId,
                    Question = question,
                    Answer = null
                }, false);
            }
            _Repository.SaveChanges();
        }

        public TestResultViewModel GetActiveUserTestResult(StartTestViewModel startModel)
        {
            var exam = _Repository.GetActiveTestResultByUser(startModel.TestId, _webUser.CurrentUser.UserId);
            if (exam == null)
                throw new BusinessLogicException("No active test found");
            return ToTestResultViewModel(exam);
        }

        public TestResultViewModel GetTestResult(TestResultParameterViewModel key)
        {
            var exam = _Repository.SelectById<TestResult>(key.TestResultId);
            if (exam == null || exam.UserId != _webUser.CurrentUser.UserId)
                throw new BusinessLogicException("Test result not found");
            return ToTestResultViewModel(exam);
        }

        private TestResultViewModel ToTestResultViewModel(TestResult exam)
        {
            var test = _Repository.SelectById<Test>(exam.TestId);
            var result = new TestResultViewModel
            {
                TestResultId = exam.TestResultId,
                TestId = test.TestId,
                TestName = test.TestName,
                TestDescription = test.TestDescription,
                TestResultBeginTime = exam.TestResultBeginTime,
                TestResultEndTime = exam.TestResultEndTime,
                TestResultStatus = exam.TestResultStatus,
            };
            if (exam.TestResultStatus == TestResultStatus.Completed)
            {
                result.TestResultScore = exam.TestResultScore;
                if (exam.TestResultRating != null)
                    result.RatingTitle = exam.TestResultRating.RatingTitle;
            }
            var userAnswers = exam.UsersAnswers.ToArray();
            result.Questions = userAnswers.GroupBy(a => a.QuestionId, (k, g) => g.First().Question)
                .OrderBy(q => q.QuestionOrder)
                .Select(q =>
                {
                    var answersForQuestion = userAnswers.Where(ua => ua.Question.QuestionId == q.QuestionId).ToArray();
                    var item = new TestResultQuestionViewModel
                    {
                        QuestionId = q.QuestionId,
                        QuestionType = q.QuestionType,
                        QuestionDescription = q.QuestionDescription,
                        QuestionText = q.QuestionText,
                        Answers = q.Answers.OrderBy(a => a.AnswerOrder).Select(a => new TestResultAnswerViewModel
                        {
                            AnswerId = a.AnswerId,
                            AnswerText = a.AnswerText
                        }).ToArray(),
                        UserAnswer = new UserAnswerViewModel
                        {
                            QuestionId = q.QuestionId,
                            TestResultId = exam.TestResultId,
                            AnswerIds = answersForQuestion
                                .Where(ua => ua.Answer != null)
                                .Select(ua => ua.Answer.AnswerId).ToArray()
                        }
                    };
                    if (answersForQuestion.Length == 1)
                        item.UserAnswer.AnswerText = answersForQuestion[0].CustomAnswerText;
                    return item;
                }).ToArray();
            return result;
        }

        public void SaveUserAnswer(UserAnswerViewModel answer)
        {
            var testResult = _Repository.SelectById<TestResult>(answer.TestResultId);
            if (testResult == null || testResult.UserId != _webUser.CurrentUser.UserId)
                throw new BusinessLogicException("Test result not found by id " + answer.TestResultId);

            var question = _Repository.SelectById<Question>(answer.QuestionId);
            if (question == null)
                throw new BusinessLogicException("Question not found by id " + answer.QuestionId);
            var oldAnswers = testResult.UsersAnswers
                .Where(a => a.Question.QuestionId == answer.QuestionId).ToArray();
            if (oldAnswers.Length == 0)
                throw new BusinessLogicException("Question is not included in the test");
            _Repository.Delete(oldAnswers, false);
            if (answer.AnswerIds.Length == 0)
            {
                _Repository.Insert(new UserAnswer
                {
                    TestResultId = answer.TestResultId,
                    Question = question,
                    Answer = null,
                    CustomAnswerText = answer.AnswerText
                }, false);
            }
            else
            {
                foreach (var answerId in answer.AnswerIds)
                {
                    var answerModel = _Repository.SelectById<Answer>(answerId);
                    if (answerModel == null || answerModel.QuestionId != answer.QuestionId)
                        throw new BusinessLogicException("Answer not found by id " + answerId);
                    _Repository.Insert<UserAnswer>(new UserAnswer
                    {
                        TestResultId = answer.TestResultId,
                        Question = question,
                        Answer = answerModel
                    }, false);
                }
            }
            _Repository.SaveChanges();
        }

        public void CompleteTestResult(TestResultParameterViewModel key)
        {
            var exam = _Repository.SelectById<TestResult>(key.TestResultId);
            if (exam.UserId != _webUser.CurrentUser.UserId)
                throw new BusinessLogicException("You can complete only owning test sessions");
            if (exam.TestResultStatus != TestResultStatus.InProgress)
                throw new BusinessLogicException("Test already completed");
            var textAnswers = false;
            var score = 0m;
            #region calculate score
            foreach (var group in exam.UsersAnswers.GroupBy(a => a.Question))
            {
                switch (group.Key.QuestionType)
                {
                    case QuestionType.TextAnswer:
                        textAnswers = true;
                        break;
                    case QuestionType.SingleAnswer:
                        var userAnswer = group.SingleOrDefault();
                        if (userAnswer != null && userAnswer.Answer != null)
                            score += userAnswer.Answer.AnswerScore;
                        break;
                    case QuestionType.MultipleAnswers:
                        var scores = group.Key.Answers.Select(a => a.AnswerScore);
                        var selected = group.Where(ua => ua.Answer != null).Sum(ua => ua.Answer.AnswerScore);
                        if (scores.Max() == 1)
                            score += selected / scores.Sum();
                        else
                            score += selected;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            #endregion
            exam.TestResultStatus = textAnswers ? TestResultStatus.Finished : TestResultStatus.Completed;
            exam.TestResultScore = score;
            exam.TestResultRating = exam.Test.Ratings.OrderByDescending(r => r.RatingLowerBound).FirstOrDefault(r => r.RatingLowerBound <= score);
            exam.TestResultEndTime = DateTime.UtcNow;
            _Repository.Update<TestResult>(exam, true);
        }


        public TestResultListItemViewModel[] GetTests()
        {
            var user = _webUser.CurrentUser;
            var userId = user.UserId;
            var groups = user.UserGroups.Select(g => g.GroupID).ToArray();
            var results = _Repository.GetTestResultsByUser(userId)
                //TODO: should be executed in repository
                .GroupBy(r => r.TestId, (testId, testResults) => testResults.OrderByDescending(r => r.TestResultBeginTime).First())
                .ToDictionary(r => r.TestId);
            var tests = _Repository.SelectAll<Test>(new Expression<Func<Test, object>>[0]);
            tests = tests.Where(t =>
                (t.TestAttributes.Any(a => a.AttributeId == (int)AttributeCode.TestIsPublic) ||
                t.UserGroups.Any(g => groups.Contains(g.GroupID))) &&
                t.TestStatus == TestStatus.Published
            );
            return tests.ToArray().Select(t =>
            {
                var model = new TestResultListItemViewModel
                {
                    TestId = t.TestId,
                    TestName = t.TestName
                };
                if (results.ContainsKey(t.TestId))
                {
                    var result = results[t.TestId];
                    model.TestResultId = result.TestResultId;
                    model.TestResultStatus = result.TestResultStatus;
                    model.TestResultScore = result.TestResultScore;
                    if (result.TestResultRating != null)
                        model.RatingTitle = result.TestResultRating.RatingTitle;
                }
                return model;
            }).ToArray();
        }
    }
}
