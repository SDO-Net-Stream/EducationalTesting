using System.Collections.Generic;
using EduTesting.Model;

namespace EduTesting.Interfaces
{
  public interface IQuestion
  {
    int QuestionId { get; set; }
    string QuestionText { get; set; }
    QuestionType QuestionType { get; set; }
    string Description { get; set; }
    ICollection<string> Answers { get; set; }
  }
}