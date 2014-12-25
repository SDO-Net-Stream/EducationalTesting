using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.ViewModels.Question;

namespace EduTesting.ViewModels.Test
{
    public class TestListItemViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int UserId { get; set; }
        public QuestionListItemViewModel[] Questions { get; set; }
    }
}
