using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduTesting.Model
{
  public class Question : IQuestion
  {
    public int QuestionId { get; set; }
    public virtual Test Test { get; set; }
    public string QuestionText { get; set; }
    public string Description { get; set; }
    public string TestType { get; set; }
    public virtual ICollection<string> Answers { get; set; }
  }

  public class FixedQuestion : Question
  {
  }
}
