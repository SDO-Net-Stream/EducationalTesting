using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Exam
{
    public class UserAnswerViewModel
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int[] AnswerIds { get; set; }
        public string AnswerText { get; set; }
    }
}
