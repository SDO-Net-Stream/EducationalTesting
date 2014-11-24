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
        TestResultListItemViewModel[] GetTestResults(TestResultsFilterViewModel filter);
        TestResultViewModel StartTest(StartTestViewModel startModel);
        TestResultViewModel GetTestResult(TestResultParameterViewModel key);
        void SaveUserAnswer(UserAnswerViewModel answer);
        void CompleteTestResult(TestResultParameterViewModel key);
    }
}
