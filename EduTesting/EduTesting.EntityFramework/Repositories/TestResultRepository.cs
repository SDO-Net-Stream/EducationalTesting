using EduTesting.Interfaces;
using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Repositories
{
    public class TestResultRepository : ITestResultRepository
    {
        private static List<TestResult> _testResults = new List<TestResult>();
        public TestResultRepository()
        {

        }

        public TestResult[] GetTestResults(int testId)
        {
            return _testResults.Where(t => t.TestId == testId).ToArray();
        }

        public TestResult GetTestResult(int testResultId)
        {
            return _testResults.FirstOrDefault(t => t.TestResultId == testResultId);
        }

        public void CreateTestResult(TestResult testResult)
        {
            testResult.TestResultId = _testResults.Any() ? _testResults.Max(t => t.TestResultId) + 1 : 1;
            _testResults.Add(testResult);
        }


        public void UpdateTestResult(TestResult testResult)
        {
            var entity = _testResults.FirstOrDefault(r => r.TestResultId == testResult.TestResultId);
            if (entity == null)
                throw new BusinessLogicException("Test result not found");
            entity.TestResultIsCompleted = testResult.TestResultIsCompleted;
            entity.TestResultScore = testResult.TestResultScore;
        }


        public void SaveUserAnswers(int testResultId, int questionId, UserAnswer[] answers)
        {
            var exam = _testResults.FirstOrDefault(r => r.TestResultId == testResultId);
            if (exam == null)
                throw new BusinessLogicException("Test result not found");
            var question = exam.Questions.FirstOrDefault(q => q.QuestionId == questionId);
            if (question == null)
                throw new BusinessLogicException("Question not found");
            var newAnswers = answers.Select(a => new UserAnswer
            {
                TestResultId = testResultId,
                QuestionId = questionId,
                AnswerId = a.AnswerId
                //TODO: text answer
            });
            exam.UserAnswers = exam.UserAnswers.Where(ua => ua.QuestionId != questionId).Union(newAnswers).ToArray();
        }
    }
}
