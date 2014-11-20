using System.Collections.Generic;

namespace EduTesting.Model
{
  public interface IQuestion
  {
    int QuestionId { get; set; }
    Test Test { get; set; }
    string QuestionText { get; set; }
    string Description { get; set; }
    string TestType { get; set; }
    ICollection<string> Answers { get; set; }
  }
}