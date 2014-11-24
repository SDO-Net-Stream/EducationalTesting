using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Interfaces
{
    public interface ITestResultRepository
    {
        TestResult[] GetTestResults(int testId);
        TestResult GetTestResult(int testResultId);
        TestResult FindActiveUserTestResult(int testId, int userId);
        void CreateTestResult(TestResult testResult);
        void SaveUserAnswers(int testResultId, int questionId, UserAnswer[] answers);
        /// <summary>
        /// Updates entity propties only.
        /// </summary>
        /// <param name="testResult"></param>
        void UpdateTestResult(TestResult testResult);
    }
}
