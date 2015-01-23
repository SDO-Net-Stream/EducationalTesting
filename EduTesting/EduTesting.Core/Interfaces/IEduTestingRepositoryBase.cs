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
        #region Test Result Repository
        IEnumerable<TestResult> GetTestResultsByTest(int testId);
        IEnumerable<TestResult> GetTestResultsByUser(int userId);
        IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId);
        TestResult GetActiveTestResultByUser(int testId, int userId);
<<<<<<< HEAD
        IEnumerable<UserAnswer> GetUserAnswersByTestResultId(int testResultId);

        void UpdateQuestionType(int questionId, int questionTypeId);
=======
        #endregion
>>>>>>> 1ca246f28d5496934918c805a0d44c7d397a1d62

        #region Test Repository
        void AddTestAttribute(int testId, AttributeCode code, string value);
        void RemoveTestAttribute(int testId, AttributeCode code);
        #endregion
    }
}
