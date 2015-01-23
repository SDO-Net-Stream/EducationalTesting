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

        public TestResultListItemViewModel[] GetTestResultsForUsers(TestResultsFilterViewModel filter)
        {
            var testResults = _Repository.GetTestResultsByTest(filter.TestId);
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                var nameFilter = filter.UserName.ToLowerInvariant();
                testResults = testResults.Where(r => string.Concat(r.User.UserFirstName, " ", r.User.UserLastName).ToLowerInvariant().Contains(filter.UserName));
            }
            return testResults
                .GroupBy(
                    r => r.UserId,
                    (rId, results) =>
                    {
                        var last = results.OrderByDescending(r => r.TestResultBeginTime).First();
                        return new TestResultListItemViewModel
                        {
                            UserFirstName = last.User.UserFirstName,
                            UserLastName = last.User.UserLastName,
                            TestResultStatus = last.TestResultStatus
                        };
                    }
                ).OrderBy(r => r.UserFirstName).ThenBy(r => r.UserLastName)
                .Take(filter.Count ?? 20)
                .ToArray();
        }
    }
}
