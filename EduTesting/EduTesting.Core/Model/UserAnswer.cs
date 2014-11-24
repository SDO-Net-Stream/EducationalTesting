using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class UserAnswer
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
    }
}
