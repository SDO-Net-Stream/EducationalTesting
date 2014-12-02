using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class UsersAnswer
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultID { get; set; }
        public int QuestionID { get; set; }
		public Nullable<int> AnswerID { get; set; }
		public string CustomAnswerText { get; set; }
    
        public virtual Answer Answers { get; set; }
        public virtual Question Questions { get; set; }
        public virtual TestsResult TestsResults { get; set; }
    }
}
