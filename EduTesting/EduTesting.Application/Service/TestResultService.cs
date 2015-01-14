using EduTesting.Interfaces;
using EduTesting.Model;
using EduTesting.ViewModels.TestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class TestResultService : ITestResultService
    {
        private readonly IEduTestingRepository _Repository;
        private readonly IWebUserManager _webUser;
        public TestResultService(IEduTestingRepository repository, IWebUserManager webUser)
        {
            _Repository = repository;
            _webUser = webUser;
        }
        public TestResultListItemViewModel[] GetTestResults(TestResultsFilterViewModel filter)
        {
            var results = _Repository.GetTestResultsByTest(filter.TestId);
            return results.Select(r => new TestResultListItemViewModel
            {
                UserFirstName = r.UserId.ToString(),
                UserLastName = r.UserId.ToString(),
                TestResultIsCompleted = r.IsCompleted
            }).ToArray();
        }

        public void StartTest(StartTestViewModel startModel)
        {
            var exam = _Repository.GetActiveTestResultByUser(startModel.TestId, _webUser.CurrentUser.UserId);
            if (exam != null)
                throw new BusinessLogicException("Test already started. Complete previous before starting new one.");
            var test = _Repository.SelectById<Test>(startModel.TestId);
            exam = new TestResult
            {
                UserId = _webUser.CurrentUser.UserId,
                TestId = test.TestId,
                Timestamp = DateTime.UtcNow,
                IsCompleted = false,
            };
            _Repository.Insert<TestResult>(exam);
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
            if (exam == null)
                throw new BusinessLogicException("Test result not found");
            return ToTestResultViewModel(exam);
        }

        private QuestionType GetQuestionType(int questionId)
        {
            var attr = _Repository.SelectById<QuestionAttribute>(questionId, EduTestingConsts.AttributeId_QuestionType);
            if (attr == null)
                throw new BusinessLogicException("Question Type not founds");
            return (QuestionType)(Int32.Parse(attr.Value));
        }

        private TestResultViewModel ToTestResultViewModel(TestResult exam)
        {
            var test = _Repository.SelectById<Test>(exam.TestId);
            var result = new TestResultViewModel
            {
                TestResultId = exam.TestResultId,
                TestId = test.TestId,
                UserId = exam.UserId,
                TestResultTimestamp = exam.Timestamp,
                TestResultEndTime = null,// exam.TestResultTimestamp.AddMinutes(test.MaxDuration)
                TestResultIsCompleted = exam.IsCompleted,
            };
            result.Questions = _Repository.GetQuestionsByTest(result.TestId).ToArray()
                .Select(q =>
            {
                var qtAttribute = q.QuestionAttributes.First(a => a.AttributeID == EduTestingConsts.AttributeId_QuestionType);
                var item = new TestResultQuestionViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionType = (QuestionType)(Int32.Parse(qtAttribute.Value)),
                    QuestionDescription = q.QuestionDescription,
                    Answers = q.Answers.Select(a => new TestResultAnswerViewModel
                    {
                        AnswerId = a.AnswerId,
                        AnswerText = a.AnswerText
                    }).ToArray(),
                    UserAnswer = new UserAnswerViewModel
                    {
                        //TODO: AnswerText
                        QuestionId = q.QuestionId,
                        TestResultId = exam.TestResultId,
                        AnswerIds = _Repository.GetUserAnswersByTestResultId(exam.TestResultId).Where(
                            ua => ua.Question.QuestionId == q.QuestionId && (ua.Answer != null)).Select(ua => ua.Answer.AnswerId).ToArray()
                    }
               };
                return item;
            }).ToArray();
            return result;
        }

        public void SaveUserAnswer(UserAnswerViewModel answer)
        {
            if (_Repository.SelectById<TestResult>(answer.TestResultId) == null)
                throw new BusinessLogicException("Test result not found by id " + answer.TestResultId);
            var question = _Repository.SelectById<Question>(answer.QuestionId);
            if (question == null)
                throw new BusinessLogicException("Question not found by id " + answer.QuestionId);
            foreach (var answerId in answer.AnswerIds)
            {
                var answerModel = _Repository.SelectById<Answer>(answerId);
                if (answerModel == null)
                    throw new BusinessLogicException("Answer not found by id " + answerId);
                _Repository.Insert<UserAnswer>(new UserAnswer
                {
                    TestResultId = answer.TestResultId,
                    Question = question,
                    Answer = answerModel
                });
            }
        }

        public void CompleteTestResult(TestResultParameterViewModel key)
        {
            var exam = _Repository.SelectById<TestResult>(key.TestResultId);
            exam.IsCompleted = true;
            _Repository.Update<TestResult>(exam);
        }


        public TestResultListItemViewModel[] GetTestResultsForCurrentUser()
        {
            var userId = _webUser.CurrentUser.UserId;
            var results = _Repository.GetTestResultsByUser(userId);
            //TODO: should be executed in repository
            var lastResults = results
                .GroupBy(r => r.TestId, (testId, testResults) => testResults.OrderByDescending(r => r.Timestamp).First())
                .ToArray();
            return lastResults.Select(r => new TestResultListItemViewModel
            {
                TestId = r.TestId,
                TestResultIsCompleted = r.IsCompleted,
                //TODO: test result score
                TestResultScore = 0
            }).ToArray();
        }
    }
}
