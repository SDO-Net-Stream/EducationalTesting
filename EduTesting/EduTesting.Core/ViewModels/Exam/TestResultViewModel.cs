using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Exam
{
    public class TestResultViewModel
    {
        public int TestResultId { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public DateTime TestResultBeginTime { get; set; }
        public DateTime? TestResultEndTime { get; set; }
        public TestResultStatus TestResultStatus { get; set; }
        public TestResultQuestionViewModel[] Questions { get; set; }

        public decimal? TestResultScore { get; set; }
        public string RatingTitle { get; set; }
    }
}
