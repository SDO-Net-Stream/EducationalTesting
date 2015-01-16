using EduTesting.ViewModels.TestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface ITestResultService
    {
        TestResultListItemViewModel[] GetTestResultsForCurrentUser();

        /// <summary>
        /// Continue if not completed or start new
        /// </summary>
        void StartTest(StartTestViewModel startModel);
        TestResultViewModel GetActiveUserTestResult(StartTestViewModel startModel);
        TestResultViewModel GetTestResult(TestResultParameterViewModel key);
        void SaveUserAnswer(UserAnswerViewModel answer);
        void CompleteTestResult(TestResultParameterViewModel key);
        TestResultListItemViewModel[] GetTestResultsForUsers(TestResultsFilterViewModel filter);
    }
}
