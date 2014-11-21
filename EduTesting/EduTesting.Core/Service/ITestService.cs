using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Controllers;
using EduTesting.Interfaces;
using EduTesting.Model;
using EduTesting.ViewModels.Test;

namespace EduTesting.Service
{
    public interface ITestService
    {
      Test GetTest(int testId);
      IEnumerable<TestListItemViewModel> GetTests();

      Test InsertTest(Test test);

      bool UpdateTest(Test test);

      void DeleteTest(TestListItemViewModel test);

      IEnumerable<IQuestion> GetQuestions(int testId);
    }
}
