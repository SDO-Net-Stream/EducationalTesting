using System.Collections.Generic;
using EduTesting.Interfaces;
using EduTesting.Model;

namespace EduTesting.Controllers
{
  public interface ITestRepository
  {
    IEnumerable<Test> GetTests();
    Test GetTest(int id);
    Test InsertTest(Test test);
    bool UpdateTest(Test test);
    bool DeleteTest(int id);

    IEnumerable<IQuestion> GetAllQuestions();
    IEnumerable<IQuestion> GetQuestions(int testId);
    IQuestion GetQuestion(int id);
    IQuestion InsertQuestion(IQuestion test, int testId);
    bool UpdateQuestion(IQuestion question);

    IEnumerable<User> GetUsers();
    User GetUser(int id);
    User InsertUser(User test);
    bool UpdateUser(User test);

    IEnumerable<Role> GetRoles();
    Role GetRole(int id);
    Role InsertRole(Role Role);
    bool UpdateRole(Role role);
  }
}