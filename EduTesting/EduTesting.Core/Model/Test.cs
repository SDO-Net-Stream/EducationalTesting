using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public TestStatus TestStatus { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestsResults { get; set; }
        public virtual ICollection<TestAttribute> TestAttributes { get; set; }
        public virtual ICollection<TestResultRating> Ratings { get; set; }
    }
}
