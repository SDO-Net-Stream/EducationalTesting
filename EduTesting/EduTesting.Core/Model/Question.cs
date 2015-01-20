using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Question
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [Required]
		public int TestId { get; set; }
		public string QuestionText { get; set; }
        public string QuestionDescription { get; set; }
        public QuestionType QuestionType { get; set; }

		public virtual Test Test { get; set; }
		public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionAttribute> QuestionAttributes { get; set; }
        public virtual ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
