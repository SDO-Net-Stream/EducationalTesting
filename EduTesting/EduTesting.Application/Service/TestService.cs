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
        private IEduTestingRepository _Repository;

        public TestService()
        {

        }

        public TestService(IEduTestingRepository _repository)
        {
            _Repository = _repository;
        }

        public TestListItemViewModel GetTest(int testId)
        {
            var test = _Repository.SelectById<Test>(testId);
            if (test == null)
                throw new BusinessLogicException("Test not found. ID = " + testId);
            return new TestListItemViewModel { TestId = test.TestId, TestName = test.TestName, Questions = GetQuestions(testId).ToArray() };
        }

        public IEnumerable<TestListItemViewModel> GetTests()
        {
            var tests = _Repository.SelectAll<Test>();
            var result = new List<TestListItemViewModel>();
            foreach(var test in tests)
            {
                result.Add(new TestListItemViewModel { TestId = test.TestId, TestName = test.TestName, Questions = GetQuestions(test.TestId).ToArray() });
            }
            return result;
        }

        public Test InsertTest(Test test)
        {
            return _Repository.Insert<Test>(test);
        }

        public void UpdateTest(Test test)
        {
            _Repository.Update<Test>(test);
        }

        public void DeleteTest(TestListItemViewModel test)
        {
            _Repository.Delete<Test>(test.TestId);
        }

        public IEnumerable<QuestionListItemViewModel> GetQuestions(int testId)
        {
            var questions = _Repository.GetQuestionsByTest(testId);
            var result = new List<QuestionListItemViewModel>();
            foreach(var question in questions)
            {
                result.Add(new QuestionListItemViewModel { QuestionId = question.QuestionId, QuestionText = question.QuestionText });
            }
            return result;
        }

        public void UpdateQuestion(Question question)
        {
            _Repository.Update<Question>(question);
        }

        public Question InsertQuestion(Question question)
        {
<<<<<<< HEAD
            return _Repository.Insert<Question>(question);
=======

            return _Repository.InsertQuestion(question, question.TestId);
>>>>>>> 92242cc6867a61bb2fd17d0fdf647e5e3794ac6c
        }

        public void DeleteQuestion(QuestionListItemViewModel question)
        {
            _Repository.Delete<Question>(question.QuestionId);
        }

        public IEnumerable<QuestionListItemViewModel> GetAllQuestions()
        {
            return _Repository.SelectAll<Question>()
                .Select(q => new QuestionListItemViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText
                })
                .ToArray();
        }
    }
}
