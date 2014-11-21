using System.Collections.Generic;
using EduTesting.Model;

namespace EduTesting.Interfaces
{
  public interface IQuestion
  {
    int QuestionId { get; set; }
    int TestId { get; set; }
    string QuestionText { get; set; }
    QuestionType QuestionType { get; set; }
    string Description { get; set; }
    List<string> Answers { get; set; }
  }
}