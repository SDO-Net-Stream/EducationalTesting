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
        public int TestID { get; set; }

        public string TestName { get; set; }

        public string TestDescription { get; set; }

        public int NumberOfQuestions { get; set; }

        public int? GroupID { get; set; }

        public virtual UserGroup UserGroups { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<TestsResult> TestsResults { get; set; }

        public virtual ICollection<CustomAttribute> Attributes { get; set; }
    }
}
