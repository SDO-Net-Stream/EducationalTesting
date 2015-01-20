using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.TestResult
{
    public class TestResultsFilterViewModel
    {
        public int TestId { get; set; }
        public string UserName { get; set; }
        public int? Count { get; set; }
    }
}
