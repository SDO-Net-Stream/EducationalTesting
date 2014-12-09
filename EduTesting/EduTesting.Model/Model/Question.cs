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
        public int QuestionID { get; set; }

        public int TestID { get; set; }

        public string QuestionText { get; set; }

        public string QuestionDescription { get; set; }

        public virtual Test Tests { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<QuestionAttribute> QuestionAttributes { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}
