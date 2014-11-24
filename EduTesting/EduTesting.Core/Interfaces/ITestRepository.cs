using System.Collections.Generic;
using EduTesting.Model;

namespace EduTesting.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<Test> GetTests();
        Test GetTest(int id);
        Test InsertTest(Test test);
        void UpdateTest(Test test);
        void DeleteTest(int id);

        IEnumerable<Question> GetAllQuestions();
        IEnumerable<Question> GetQuestions(int testId);
        Question GetQuestion(int id);
        Question InsertQuestion(Question test, int testId);
        void UpdateQuestion(Question question);

        IEnumerable<User> GetUsers();
        User GetUser(int id);
        User InsertUser(User test);
        void UpdateUser(User test);

        IEnumerable<Role> GetRoles();
        Role GetRole(int id);
        Role InsertRole(Role Role);
        void UpdateRole(Role role);
        void DeleteQuestion(int questionId);
    }
}