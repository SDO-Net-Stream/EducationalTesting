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
    public class EduTestingRepository : IEduTestingRepository
	{
		#region Constructor_Dispose

		public EduTestingRepository()
		{
			DBContext = new EduTestingDbContext();
		}

        public EduTestingRepository(EduTestingDbContext db)
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
            return DBContext.Set<TEntity>().Add(item);
        }

        public IEnumerable<TEntity> Insert<TEntity>(IEnumerable<TEntity> items) where TEntity : class
        {
            return DBContext.Set<TEntity>().AddRange(items);
        }

        public void Update<TEntity>(TEntity item) where TEntity : class
        {
            DBContext.Set<TEntity>().Attach(item);
            DBContext.Entry(item).State = EntityState.Modified;
        }

        public void Update<TEntity>(IEnumerable<TEntity> items) where TEntity : class
		{
            foreach(var item in items)
            {
                Update(item);
            }
		}

        public void Delete<TEntity>(int itemId) where TEntity : class
        {
            var table = DBContext.Set<TEntity>();
            var item = table.Find(itemId);
            table.Remove(item);
        }

        public void Delete<TEntity>(IEnumerable<TEntity> items) where TEntity : class
		{
            DBContext.Set<TEntity>().RemoveRange(items);
		}

        #endregion

        #region Specifuc Methods

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

        #endregion

        #region Members

        protected EduTestingDbContext DBContext { get; private set; }

		#endregion
	}
}
