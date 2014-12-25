using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class TestResult
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TestId { get; set; }
        public bool IsCompleted { get; set; }
        public System.DateTime Timestamp { get; set; }
    
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
