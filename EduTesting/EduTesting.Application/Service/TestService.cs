using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Question;
using EduTesting.ViewModels.Test;
using EduTesting.Interfaces;

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

        public IEnumerable<Test> GetTests()
        {
            return _Repository.GetTests().ToArray();
        }

        public Test InsertTest(Test test)
        {
            return _Repository.InsertTest(test);
        }

        public void UpdateTest(Test test)
        {
            _Repository.UpdateTest(test);
        }

        public void DeleteTest(TestListItemViewModel test)
        {
            _Repository.DeleteTest(test.TestId);
        }

        public IEnumerable<Question> GetQuestions(int testId)
        {
            return _Repository.GetQuestions(testId);
        }

        public void UpdateQuestion(Question question)
        {
            _Repository.UpdateQuestion(question);
        }

        public Question InsertQuestion(Question question)
        {
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
