using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class TestsResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestResultID { get; set; }

        public int UserID { get; set; }

        public int TestID { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime Timestamp { get; set; }
    
        public virtual Test Tests { get; set; }

        public virtual User Users { get; set; }

        public virtual ICollection<UsersAnswer> UsersAnswers { get; set; }
    }
}
