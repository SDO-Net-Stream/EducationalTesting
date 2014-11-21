using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Interfaces;

namespace EduTesting.Model
{
  public enum TestType
  {
    Random, Ordered, Standard
  }

  public class Test
  {
      public int TestId { get; set; }
      public string TestName { get; set; }
      public int UserId { get; set; }
      public virtual List<IQuestion> Questions { get; set; }
    }
}
