using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Answer
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }
        [Required]
		public int QuestionId { get; set; }
		public string AnswerText { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual ICollection<CustomAttribute> Attributes { get; set; }
    }
}
