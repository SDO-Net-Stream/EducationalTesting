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
        
        public DateTime TestResultBeginTime { get; set; }
        /// <summary>
        /// Start_time + max_duration or closing time
        /// </summary>
        public DateTime? TestResultEndTime { get; set; }
        public TestResultStatus TestResultStatus { get; set; }
        public decimal TestResultScore { get; set; }

        public virtual TestResultRating TestResultRating { get; set; }
        public virtual Test Test { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<UserAnswer> UsersAnswers { get; set; }
    }
}
