using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class UserAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAnswerId { get; set; }
        [Required]
        public int TestResultId { get; set; }
		public string CustomAnswerText { get; set; }

        public virtual TestResult TestResult { get; set; }
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
