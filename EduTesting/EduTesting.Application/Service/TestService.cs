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

        public IEnumerable<TestListItemViewModel> GetTests()
        {
            return _Repository.GetTests()
                .Select(t => new TestListItemViewModel
                {
                    Id = t.TestId,
                    Name = t.TestName
                })
                .ToArray();
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

        public bool UpdateQuestion(IQuestion question)
        {
            return _Repository.UpdateQuestion(question);
        }

        public IQuestion InsertQuestion(IQuestion question, int testId)
        {
            return _Repository.InsertQuestion(question, testId);
        }
    }
}
