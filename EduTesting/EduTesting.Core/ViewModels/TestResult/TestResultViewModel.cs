using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.TestResult
{
    public class TestResultViewModel
    {
        public int TestResultId { get; set; }
        public int UserId { get; set; }
        public DateTime TestResultTimestamp { get; set; }
        public DateTime? TestResultEndTime { get; set; }
        public bool TestResultIsCompleted { get; set; }
        public decimal TestResultScore { get; set; }
        public TestResultQuestionViewModel[] Questions { get; set; }
    }
}
