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
        private readonly ITestResultRepository _testResultRepository;
        private readonly ITestRepository _testRepository;
        private readonly IWebUserManager _webUser;
        public TestResultService(ITestResultRepository testResultRepository, ITestRepository testRepository, IWebUserManager webUser)
        {
            _testResultRepository = testResultRepository;
            _testRepository = testRepository;
            _webUser = webUser;
        }
        public TestResultListItemViewModel[] GetTestResults(TestResultsFilterViewModel filter)
        {
            var results = _testResultRepository.GetTestResults(filter.TestId);
            return results.Select(r => new TestResultListItemViewModel
            {
                UserFirstName = r.UserId.ToString(),
                UserLastName = r.UserId.ToString(),
                TestResultIsCompleted = r.TestResultIsCompleted,
                TestResultScore = r.TestResultScore
            }).ToArray();
        }

        public void StartTest(StartTestViewModel startModel)
        {
            var exam = _testResultRepository.FindActiveUserTestResult(startModel.TestId, _webUser.CurrentUser.UserId);
            if (exam != null)
                throw new BusinessLogicException("Test already started. Complete previous before starting new one.");
            var test = _testRepository.GetTest(startModel.TestId);
            exam = new TestResult
            {
                UserId = _webUser.CurrentUser.UserId,
                TestId = test.TestId,
                TestResultTimestamp = DateTime.UtcNow,
                TestResultIsCompleted = false,
                TestResultScore = 0,
                UserAnswers = new UserAnswer[0]
            };
            exam.Questions = test.Questions;
            _testResultRepository.CreateTestResult(exam);
        }

        public TestResultViewModel GetActiveUserTestResult(StartTestViewModel startModel)
        {
            var exam = _testResultRepository.FindActiveUserTestResult(startModel.TestId, _webUser.CurrentUser.UserId);
            if (exam == null)
                throw new BusinessLogicException("No active test found");
            return ToTestResultViewModel(exam);
        }

        public TestResultViewModel GetTestResult(TestResultParameterViewModel key)
        {
            var exam = _testResultRepository.GetTestResult(key.TestResultId);
            if (exam == null)
                throw new BusinessLogicException("Test result not found");
            return ToTestResultViewModel(exam);
        }

        private TestResultViewModel ToTestResultViewModel(TestResult exam)
        {
            var test = _testRepository.GetTest(exam.TestId);
            var result = new TestResultViewModel
            {
                TestResultId = exam.TestResultId,
                UserId = exam.UserId,
                TestResultTimestamp = exam.TestResultTimestamp,
                TestResultEndTime = null,// exam.TestResultTimestamp.AddMinutes(test.MaxDuration)
                TestResultIsCompleted = exam.TestResultIsCompleted,
                TestResultScore = exam.TestResultScore
            };
            result.Questions = exam.Questions.Select(q => new TestResultQuestionViewModel
            {
                QuestionId = q.QuestionId,
                QuestionType = q.QuestionType,
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
                    AnswersId = exam.UserAnswers.Where(ua => ua.QuestionId == q.QuestionId && ua.AnswerId.HasValue).Select(ua => ua.AnswerId.Value).ToArray()
                }
            }).ToArray();
            return result;
        }

        public void SaveUserAnswer(UserAnswerViewModel answer)
        {
            var answers = answer.AnswersId.Select(id => new UserAnswer
            {
                AnswerId = id,
                QuestionId = answer.QuestionId,
                TestResultId = answer.TestResultId
            }).ToArray();
            _testResultRepository.SaveUserAnswers(answer.TestResultId, answer.QuestionId, answers);
        }

        public void CompleteTestResult(TestResultParameterViewModel key)
        {
            var exam = _testResultRepository.GetTestResult(key.TestResultId);
            exam.TestResultIsCompleted = true;
            exam.TestResultScore = 2;// TODO: calculate
            _testResultRepository.UpdateTestResult(exam);
        }
    }
}
