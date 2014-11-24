using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class TestResult
    {
        public int TestResultId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime TestResultTimestamp { get; set; }
        public bool TestResultIsCompleted { get; set; }
        public decimal TestResultScore { get; set; }
        public Question[] Questions { get; set; }
        public UserAnswer[] UserAnswers { get; set; }
    }
}
