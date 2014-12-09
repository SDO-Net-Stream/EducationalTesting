using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public enum QuestionType
    {
        SingleAnswer, MultipleAnswers, TextAnswer
    }

    public class Question
    {
	    public Question()
	    {
			//TODO: remove for testing purpose
		    Answers = new List<Answer>();
			Answers.Add(new Answer());
			Answers.Add(new Answer());
			Answers.Add(new Answer());
	    }

	    public int QuestionId { get; set; }
        public int TestId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string QuestionDescription { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}