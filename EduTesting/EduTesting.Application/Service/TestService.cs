using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Test;
using EduTesting.Interfaces;

namespace EduTesting.Service
{
    public class TestService : EduTestingAppServiceBase, ITestService
    {
        private IEduTestingRepository _Repository;

        public TestService(IEduTestingRepository _repository)
        {
            _Repository = _repository;
        }

        public TestViewModel GetTest(int testId)
        {
            var test = _Repository.SelectById<Test>(testId);
            if (test == null)
                throw new BusinessLogicException("Test not found. ID = " + testId);
            return new TestViewModel
            {
                TestId = test.TestId,
                TestName = test.TestName,
                Questions = test.Questions.Select(question => new QuestionViewModel
                {
                    TestId = testId,
                    QuestionId = question.QuestionId,
                    QuestionText = question.QuestionText
                }).ToArray()
            };
        }

        public IEnumerable<TestListItemViewModel> GetTests()
        {
            var tests = _Repository.SelectAll<Test>();
            var result = new List<TestListItemViewModel>();
            foreach (var test in tests)
            {
                result.Add(new TestListItemViewModel { TestId = test.TestId, TestName = test.TestName });
            }
            return result;
        }

        public TestViewModel InsertTest(TestViewModel test)
        {
            var t = _Repository.Insert<Test>(new Test { TestId = test.TestId, TestName = test.TestName });
            test.TestId = t.TestId;
            return test;
        }

        public void UpdateTest(TestViewModel test)
        {
            _Repository.Update<Test>(new Test { TestId = test.TestId, TestName = test.TestName });
        }

        public void DeleteTest(TestViewModel test)
        {
            _Repository.Delete<Test>(test.TestId);
        }
        /*
        public IEnumerable<QuestionListItemViewModel> GetQuestions(int testId)
        {
            var questions = _Repository.GetQuestionsByTest(testId);
            var result = new List<QuestionListItemViewModel>();
            foreach(var question in questions)
            {
                result.Add(new QuestionListItemViewModel 
                {
                    TestId = testId,
                    QuestionId = question.QuestionId, 
                    QuestionText = question.QuestionText 
                });
            }
            return result;
        }

        public void UpdateQuestion(Question question)
        {
            _Repository.Update<Question>(question);
        }

        public QuestionListItemViewModel InsertQuestion(QuestionListItemViewModel question)
        {
            var q = _Repository.Insert<Question>(new Question
            {
                TestId = question.TestId,
                QuestionText = question.QuestionText,
                QuestionDescription = question.QuestionDescription
            });
            question.QuestionId = q.QuestionId;
            _Repository.Insert<QuestionAttribute>(new QuestionAttribute
            {
                QuestionID = q.QuestionId, 
                AttributeID = EduTestingConsts.AttributeId_QuestionType, 
                Value = ((int)(question.QuestionType)).ToString()
            });
            return question;
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
                    TestId = q.TestId,
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText
                })
                .ToArray();
        }
         */
    }
}
