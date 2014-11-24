using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Controllers;
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
                    TestId = t.TestId,
                    TestName = t.TestName
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

        public void DeleteTest(TestListItemViewModel test)
        {
            _Repository.DeleteTest(test.TestId);
        }

        public IEnumerable<Question> GetQuestions(int testId)
        {
            return _Repository.GetQuestions(testId);
        }

        public bool UpdateQuestion(Question question)
        {
            return _Repository.UpdateQuestion(question);
        }

        public Question InsertQuestion(Question question)
        {
            //if (question.TestId )
            return _Repository.InsertQuestion(question, 2);
        }

        public void DeleteQuestion(QuestionListItemViewModel question)
        {
            _Repository.DeleteQuestion(question.QuestionId);
        }

        public IEnumerable<QuestionListItemViewModel> GetAllQuestions()
        {
            return _Repository.GetAllQuestions()
                .Select(q => new QuestionListItemViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText
                })
                .ToArray();
        }
    }
}
