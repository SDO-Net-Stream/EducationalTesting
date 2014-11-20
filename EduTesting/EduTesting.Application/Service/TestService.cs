using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Controllers;
using EduTesting.Model;

namespace EduTesting.Service
{

    public class TestService : EduTestingAppServiceBase, ITestService
    {
        private ITestRepository _Repository;

        public TestService(ITestRepository _repository)
        {
          _Repository = _repository;
        }

        public Test GetTest(int testId)
        {
          return _Repository.GetTest(testId);
        }
        
        public  IEnumerable<Test> GetTests()
        {
          return _Repository.GetTests();
        }

        public Test GetTestById(int id)
        {
          return _Repository.GetTest(id);
        }

        public Test InsertTest(Test test)
        {
          return _Repository.InsertTest(test);
        }

        public bool UpdateTest(Test test)
        {
          return _Repository.UpdateTest(test);
        }

        public bool DeleteTest(int id)
        {
          return _Repository.DeleteTest(id);
        }

        public IEnumerable<IQuestion> GetQuestions(int testId)
        {
          return _Repository.GetQuestions(testId);
        }
    }
}
