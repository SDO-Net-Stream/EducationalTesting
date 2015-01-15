using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EduTesting.EntityFramework;
using EduTesting.Interfaces;
using EduTesting.Model;

namespace EduTesting.Repositories
{
    public class EduTestingGenericRepository : IEduTestingGenericRepository
    {
        #region Constructor_Dispose

        public EduTestingGenericRepository()
        {
            DBContext = new EduTestingDbContext();
        }

        public EduTestingGenericRepository(EduTestingDbContext db)
        {
            DBContext = db;
        }

        public void Dispose()
        {
            if (DBContext != null)
            {
                DBContext.Dispose();
            }
        }

        #endregion

        #region Generic Methods

        public IQueryable<TEntity> SelectAll<TEntity>(params Expression<Func<TEntity, object>>[] includeObjects) where TEntity : class
        {
            IQueryable<TEntity> resultList = DBContext.Set<TEntity>();

            foreach (var includeItem in includeObjects)
            {
                resultList = resultList.Include(includeItem);
            }

            return resultList;
        }

        public IEnumerable<TEntity> SelectAll<TEntity>() where TEntity : class
        {
            return DBContext.Set<TEntity>().ToList();
        }

        public TEntity SelectById<TEntity>(params object[] keyValues) where TEntity : class
        {
            return DBContext.Set<TEntity>().Find(keyValues);
        }

        public TEntity Insert<TEntity>(TEntity item) where TEntity : class
        {
            var entity = DBContext.Set<TEntity>().Add(item);
            DBContext.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> Insert<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            var entities = DBContext.Set<TEntity>().AddRange(items);
            DBContext.SaveChanges();
            return entities;
        }

        private void UpdateInternal<TEntity>(TEntity item) where TEntity : class
        {
            DBContext.Set<TEntity>().Attach(item);
            DBContext.Entry(item).State = EntityState.Modified;
        }

        public void Update<TEntity>(TEntity item) where TEntity : class
        {
            UpdateInternal<TEntity>(item);
            DBContext.SaveChanges();
        }

        public void Update<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            foreach(var item in items)
            {
                UpdateInternal<TEntity>(item);
            }
            DBContext.SaveChanges();
        }

        public void Delete<TEntity>(int itemId) where TEntity : class
        {
            var table = DBContext.Set<TEntity>();
            var item = table.Find(itemId);
            table.Remove(item);
            DBContext.SaveChanges();
        }

        public void Delete<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            DBContext.Set<TEntity>().RemoveRange(items);
            DBContext.SaveChanges();
        }

        #endregion

        #region Properties

        protected EduTestingDbContext DBContext { get; private set; }

        #endregion
    }

    public class EduTestingRepository : EduTestingGenericRepository, IEduTestingRepository
    {
        #region Methods

        public IEnumerable<Question> GetQuestionsByTest(int testId)
        {
            return DBContext.Questions.Where(q => q.TestId == testId);
        }

        public IEnumerable<TestResult> GetTestResultsByTest(int testId)
        {
            return DBContext.TestResults.Where(tr => tr.TestId == testId);
        }

        public IEnumerable<TestResult> GetTestResultsByUser(int userId)
        {
            return DBContext.TestResults.Where(tr => tr.UserId == userId);
        }

        public IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId)
        {
            return DBContext.TestResults.Where(tr => tr.TestId == testId && tr.UserId == userId);
        }

        public TestResult GetActiveTestResultByUser(int testId, int userId)
        {
            return DBContext.TestResults.FirstOrDefault(tr => tr.TestId == testId && tr.UserId == userId && tr.IsCompleted == false);
        }

        public IEnumerable<UserAnswer> GetUserAnswersByTestResultId(int testsResultId)
        {
            return DBContext.UsersAnswers.Where(ua => ua.TestResult.TestResultId == testsResultId);
        }

        public void UpdateQuestionType(int questionId, int questionTypeId)
        {
            var attr = DBContext.QuestionAttributes.FirstOrDefault(qa => qa.QuestionID == questionId && qa.AttributeID == EduTestingConsts.AttributeId_QuestionType);
            if (attr == null)
            {
                attr = new QuestionAttribute
                {
                    AttributeID = EduTestingConsts.AttributeId_QuestionType,
                    QuestionID = questionId
                };
                DBContext.QuestionAttributes.Add(attr);
            }
            attr.Value = questionTypeId.ToString();
            DBContext.SaveChanges();
        }

        #endregion


    }
}
