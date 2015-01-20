using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduTesting.ViewModels.TestResult
{
    public class TestResultListItemViewModel
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public int TestId { get; set; }
        public TestResultStatus TestResultStatus { get; set; }
        public decimal TestResultScore { get; set; }
    }
}
