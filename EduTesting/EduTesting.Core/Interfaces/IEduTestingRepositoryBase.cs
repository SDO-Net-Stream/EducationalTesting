using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EduTesting.Model;

namespace EduTesting.Interfaces
{
    public interface IEduTestingGenericRepository : IDisposable
    {
        IQueryable<TEntity> SelectAll<TEntity>(params Expression<Func<TEntity, object>>[] includeObjects) where TEntity : class;
        IEnumerable<TEntity> SelectAll<TEntity>() where TEntity : class;
        TEntity SelectById<TEntity>(params object[] keyValues) where TEntity : class;
        
        TEntity Insert<TEntity>(TEntity item) where TEntity : class;
        IEnumerable<TEntity> Insert<TEntity>(IEnumerable<TEntity> items) where TEntity : class;

        void Update<TEntity>(TEntity item) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> items) where TEntity : class;

        void Delete<TEntity>(int itemId) where TEntity : class;
        void Delete<TEntity>(IEnumerable<TEntity> items) where TEntity : class;
    }

    public interface IEduTestingRepository : IEduTestingGenericRepository
    {
        IEnumerable<Question> GetQuestionsByTest(int testId);

        IEnumerable<TestResult> GetTestResultsByTest(int testId);
        IEnumerable<TestResult> GetTestResultsByUser(int userId);
        IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId);
        TestResult GetActiveTestResultByUser(int testId, int userId);
        IEnumerable<UserAnswer> GetUserAnswersByTestResultId(int testResultId);
        void UpdateQuestionType(int questionId, int questionTypeId);

    }
}
