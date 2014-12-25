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
        public int NumberOfQuestions { get; set; }
        public Nullable<int> GroupId { get; set; }

		public virtual UserGroup UserGroup { get; set; }
		public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestsResults { get; set; }
        public virtual ICollection<CustomAttribute> Attributes { get; set; }
    }
}
