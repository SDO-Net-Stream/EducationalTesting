using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EduTesting.Interfaces;

namespace EduTesting.Model
{
    public enum QuestionType
    {
        Radio, Checkbox, Textbox
    }

    public class Question : IQuestion
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public QuestionType QuestionType { get; set; }
        public virtual List<string> Answers { get; set; }
    }

    public class FixedQuestion : Question
    {
    }

    public class EnterTextQuestion : Question
    {
    }
}
