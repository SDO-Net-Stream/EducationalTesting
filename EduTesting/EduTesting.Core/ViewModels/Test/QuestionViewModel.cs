using EduTesting.ViewModels.TestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Test
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string QuestionDescription { get; set; }

    }
}
