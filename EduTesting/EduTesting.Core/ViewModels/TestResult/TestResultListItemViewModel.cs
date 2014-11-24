using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduTesting.ViewModels.TestResult
{
    public class TestResultListItemViewModel
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public bool TestResultIsCompleted { get; set; }
        public decimal TestResultScore { get; set; }
    }
}
