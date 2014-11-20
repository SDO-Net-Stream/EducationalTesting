using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Controllers;
using EduTesting.Interfaces;
using EduTesting.Model;

namespace EduTesting.Service
{
    public interface ITestService
    {
      Test GetTest(int testId);
      IEnumerable<Test> GetTests();

      Test GetTestById(int id);

      Test InsertTest(Test test);

      bool UpdateTest(Test test);

      bool DeleteTest(int id);

      IEnumerable<IQuestion> GetQuestions(int testId);
    }
}
