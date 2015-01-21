using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduTesting.ViewModels.Exam
{
    public class TestResultListItemViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public TestResultStatus? TestResultStatus { get; set; }
        public decimal? TestResultScore { get; set; }
        public string RatingTitle { get; set; }
    }
}
