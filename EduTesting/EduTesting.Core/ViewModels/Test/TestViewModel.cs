using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Test
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public bool TestIsPublic { get; set; }
        public int? TestTimeLimit { get; set; }
        public int? TestRandomSubsetSize { get; set; }
        public TestStatus TestStatus { get; set; }

        public QuestionViewModel[] Questions { get; set; }
        public TestResultRatingViewModel[] Ratings { get; set; }
    }
}
