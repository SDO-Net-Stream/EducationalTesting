using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EduTesting.Model;
using EduTesting.DataProvider;

namespace EduTesting.Interfaces
{
    public interface IEduTestingGenericRepository
    {
        IQueryable<TEntity> SelectAll<TEntity>(params Expression<Func<TEntity, object>>[] includeObjects) where TEntity : class;
        IEnumerable<TEntity> SelectAll<TEntity>() where TEntity : class;
        TEntity SelectById<TEntity>(params object[] keyValues) where TEntity : class;

        TEntity Insert<TEntity>(TEntity item, bool saveChanges) where TEntity : class;
        IEnumerable<TEntity> Insert<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class;

        void Update<TEntity>(TEntity item, bool saveChanges) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class;

        void Delete<TEntity>(int itemId, bool saveChanges) where TEntity : class;
        void Delete<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class;

        void SaveChanges();
    }

    public interface IEduTestingRepository : IEduTestingGenericRepository, IUserRepository
    {
        IEnumerable<Question> GetQuestionsByTest(int testId);

        IEnumerable<TestResult> GetTestResultsByTest(int testId);
        IEnumerable<TestResult> GetTestResultsByUser(int userId);
        IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId);
        TestResult GetActiveTestResultByUser(int testId, int userId);
        IEnumerable<UserAnswer> GetUserAnswersByTestResultId(int testResultId);

        void UpdateQuestionType(int questionId, int questionTypeId);

        void UpdateAnswerIsRight(int answerId, bool isRight);
    }
}
