using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduTesting.ViewModels.Exam
{
    public class TestResultQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionDescription { get; set; }
        public QuestionType QuestionType { get; set; }
        public TestResultAnswerViewModel[] Answers { get; set; }
        public UserAnswerViewModel UserAnswer { get; set; }
    }
}
