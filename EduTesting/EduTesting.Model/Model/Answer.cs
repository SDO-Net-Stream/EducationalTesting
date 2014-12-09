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
        public int AnswerID { get; set; }

        public int QuestionID { get; set; }

        public string AnswerText { get; set; }
    
        public virtual Question Questions { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }

        public virtual ICollection<CustomAttribute> Attributes { get; set; }
    }
}
