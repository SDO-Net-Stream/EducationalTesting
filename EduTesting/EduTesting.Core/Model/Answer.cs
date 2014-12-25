using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Answer
    {
<<<<<<< HEAD
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
=======
		//TODO: remove for testing purpose
	    public Answer()
	    {
		    
	    }

        public Answer(int answerPosition, string answerText, bool isRight = false)
        {
            AnswerId = answerPosition;
            AnswerPosition = answerPosition;
            AnswerText = answerText;
            AnswerIsRight = isRight;
        }

>>>>>>> 92242cc6867a61bb2fd17d0fdf647e5e3794ac6c
        public int AnswerId { get; set; }
        [Required]
		public int QuestionId { get; set; }
		public string AnswerText { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual ICollection<CustomAttribute> Attributes { get; set; }
    }
}
