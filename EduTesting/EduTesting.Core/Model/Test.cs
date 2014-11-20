using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
  public class Test
  {
      public int TestId { get; set; }
      public string TestName { get; set; }
      public int UserId { get; set; }
      public virtual ICollection<IQuestion> Questions { get; set; }
    }
}
