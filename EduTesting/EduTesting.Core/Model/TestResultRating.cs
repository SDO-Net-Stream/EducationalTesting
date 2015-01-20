using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    /// <summary>
    /// Interpretation of test result
    /// </summary>
    public class TestResultRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }
        [Required]
        public int TestId { get; set; }
        public int RatingLowerBound { get; set; }
        public string RatingTitle { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
