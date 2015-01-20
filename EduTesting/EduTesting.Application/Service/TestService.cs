using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.ViewModels.Test;
using EduTesting.Interfaces;
using EduTesting.ViewModels.TestResult;

namespace EduTesting.Service
{
    public class TestService : EduTestingAppServiceBase, ITestService
    {
        private IEduTestingRepository _Repository;

        public TestService(IEduTestingRepository _repository)
        {
            _Repository = _repository;
        }

        public TestViewModel GetTest(TestListItemViewModel test)
        {
            var entity = _Repository.SelectById<Test>(test.TestId);
            if (entity == null)
                throw new BusinessLogicException("Test not found. ID = " + test.TestId);
            var result = new TestViewModel
            {
                TestId = entity.TestId,
                TestName = entity.TestName
            };
            if (entity.Questions == null)
            {
                result.Questions = new QuestionViewModel[0];
            }
            else
            {
                result.Questions = entity.Questions.Select(question =>
                {
                    var model = new QuestionViewModel
                    {
                        TestId = entity.TestId,
                        QuestionId = question.QuestionId,
                        QuestionText = question.QuestionText,
                        QuestionDescription = question.QuestionDescription,
                        QuestionType = question.QuestionType
                    };
                    if (question.Answers == null)
                        model.Answers = new AnswerViewModel[0];
                    else
                    {
                        model.Answers = question.Answers.OrderBy(a => a.AnswerOrder).Select(a =>
                        {
                            var answer = new AnswerViewModel
                            {
                                AnswerId = a.AnswerId,
                                AnswerText = a.AnswerText,
                                AnswerScore = a.AnswerScore
                            };
                            return answer;
                        }).ToArray();
                    }
                    return model;
                }).ToArray();
            }
            if (entity.Ratings == null)
            {
                result.Ratings = new TestResultRatingViewModel[0];
            }
            else
            {
                result.Ratings = entity.Ratings.Select(rating => new TestResultRatingViewModel
                {
                    RatingId = rating.RatingId,
                    RatingLowerBound = rating.RatingLowerBound,
                    RatingTitle = rating.RatingTitle
                }).ToArray();
            }
            return result;
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
            var entity = new Test();
            UpdateTestPropertiesFromViewModel(test, entity);
            entity = _Repository.Insert<Test>(entity);
            UpdateTestQuestionsFromViewModel(test, entity);
            UpdateRatingsFromViewModel(test, entity);
            test.TestId = entity.TestId;
            return test;
        }

        public void UpdateTest(TestViewModel test)
        {
            var entity = _Repository.SelectById<Test>(test.TestId);
            UpdateTestPropertiesFromViewModel(test, entity);
            _Repository.Update(entity);
            UpdateTestQuestionsFromViewModel(test, entity);
            UpdateRatingsFromViewModel(test, entity);
        }
        #region Update Test
        private void UpdateTestPropertiesFromViewModel(TestViewModel model, Test entity)
        {
            entity.TestName = model.TestName;
            entity.TestDescription = model.TestDescription;
        }
        private void UpdateTestQuestionsFromViewModel(TestViewModel model, Test entity)
        {
            var toUpdate = (entity.Questions ?? new Question[0]).ToDictionary(q => q.QuestionId);
            foreach (var question in model.Questions)
            {
                Question newQuestion;
                if (toUpdate.ContainsKey(question.QuestionId))
                {
                    newQuestion = toUpdate[question.QuestionId];
                    toUpdate.Remove(question.QuestionId);
                }
                else
                {
                    newQuestion = new Question();
                    newQuestion.TestId = entity.TestId;
                    _Repository.Insert(newQuestion);
                    entity.Questions.Add(newQuestion);
                }
                newQuestion.QuestionText = question.QuestionText;
                newQuestion.QuestionDescription = question.QuestionDescription;
                newQuestion.QuestionType = question.QuestionType;
                _Repository.Update(newQuestion);
                UpdateTestAnswersFromViewModel(question, newQuestion);
            }
            foreach (var question in toUpdate)
            {
                entity.Questions.Remove(question.Value);
                _Repository.Delete<Question>(question.Value.QuestionId);
            }
        }

        private void UpdateTestAnswersFromViewModel(QuestionViewModel model, Question entity)
        {
            var toUpdate = (entity.Answers ?? new Answer[0]).ToDictionary(a => a.AnswerId);
            for (var i=0;i<model.Answers.Length;i++)
            {
                var answer = model.Answers[i];
                Answer newAnswer;
                if (toUpdate.ContainsKey(answer.AnswerId))
                {
                    newAnswer = toUpdate[answer.AnswerId];
                    toUpdate.Remove(answer.AnswerId);
                }
                else
                {
                    newAnswer = new Answer();
                    newAnswer.QuestionId = entity.QuestionId;
                    _Repository.Insert(newAnswer);
                    entity.Answers.Add(newAnswer);
                }
                newAnswer.AnswerText = answer.AnswerText;
                newAnswer.AnswerScore = answer.AnswerScore;
                newAnswer.AnswerOrder = i;
            }
            foreach (var answer in toUpdate)
            {
                entity.Answers.Remove(answer.Value);
                _Repository.Delete<Answer>(answer.Value.QuestionId);
            }
        }

        private void UpdateRatingsFromViewModel(TestViewModel model, Test entity)
        {
            var toUpdate = (entity.Ratings ?? new TestResultRating[0]).ToDictionary(r => r.RatingId);
            foreach (var rating in model.Ratings)
            {
                TestResultRating newRating;
                if (toUpdate.ContainsKey(rating.RatingId))
                {
                    newRating = toUpdate[rating.RatingId];
                    toUpdate.Remove(rating.RatingId);
                }
                else
                {
                    newRating = new TestResultRating();
                    newRating.TestId = model.TestId;
                    _Repository.Insert(newRating);
                    entity.Ratings.Add(newRating);
                }
                newRating.RatingTitle = rating.RatingTitle;
                newRating.RatingLowerBound = rating.RatingLowerBound;
                _Repository.Update(newRating);
            }
            foreach (var rating in toUpdate)
            {
                entity.Ratings.Remove(rating.Value);
                _Repository.Delete<TestResultRating>(rating.Value.RatingId);
            }
        }

        #endregion

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
