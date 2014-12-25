using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Question
    {
<<<<<<< HEAD
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [Required]
		public int TestId { get; set; }
		public string QuestionText { get; set; }
=======
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
>>>>>>> 92242cc6867a61bb2fd17d0fdf647e5e3794ac6c
        public string QuestionDescription { get; set; }

		public virtual Test Test { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionAttribute> QuestionAttributes { get; set; }
        public virtual ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
