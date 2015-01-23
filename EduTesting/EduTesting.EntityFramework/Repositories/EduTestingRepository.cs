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
        }

        protected EduTestingDbContext GetDBContext()
        {
            return Abp.Dependency.IocManager.Instance.Resolve<EduTestingDbContext>();
        }

        #endregion

        #region Generic Methods

        public IQueryable<TEntity> SelectAll<TEntity>(params Expression<Func<TEntity, object>>[] includeObjects) where TEntity : class
        {
            IQueryable<TEntity> resultList = GetDBContext().Set<TEntity>();

            foreach (var includeItem in includeObjects)
            {
                resultList = resultList.Include(includeItem);
            }

            return resultList;
        }

        public IEnumerable<TEntity> SelectAll<TEntity>() where TEntity : class
        {
            return GetDBContext().Set<TEntity>().ToList();
        }

        public TEntity SelectById<TEntity>(params object[] keyValues) where TEntity : class
        {
            return GetDBContext().Set<TEntity>().Find(keyValues);
        }

        public TEntity Insert<TEntity>(TEntity item, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            var entity = db.Set<TEntity>().Add(item);
            db.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> Insert<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            var entities = db.Set<TEntity>().AddRange(items);
            if (saveChanges)
                db.SaveChanges();
            return entities;
        }

        private void UpdateInternal<TEntity>(EduTestingDbContext db, TEntity item) where TEntity : class
        {
            db.Set<TEntity>().Attach(item);
            db.Entry(item).State = EntityState.Modified;
        }

        public void Update<TEntity>(TEntity item, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            UpdateInternal<TEntity>(db, item);
            if (saveChanges)
                db.SaveChanges();
        }

        public void Update<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            foreach (var item in items)
            {
                UpdateInternal<TEntity>(db, item);
            }
            if (saveChanges)
                db.SaveChanges();
        }

        public void Delete<TEntity>(int itemId, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            var table = db.Set<TEntity>();
            var item = table.Find(itemId);
            table.Remove(item);
            if (saveChanges)
                db.SaveChanges();
        }

        public void Delete<TEntity>(IEnumerable<TEntity> items, bool saveChanges) where TEntity : class
        {
            var db = GetDBContext();
            db.Set<TEntity>().RemoveRange(items);
            if (saveChanges)
                db.SaveChanges();
        }

        public void SaveChanges()
        {
            GetDBContext().SaveChanges();
        }

        #endregion
    }

    public partial class EduTestingRepository : EduTestingGenericRepository, IEduTestingRepository
    {
        #region Methods
        
        public IEnumerable<TestResult> GetTestResultsByTest(int testId)
        {
            return GetDBContext().TestResults.Where(tr => tr.TestId == testId);
        }

        public IEnumerable<TestResult> GetTestResultsByUser(int userId)
        {
            return GetDBContext().TestResults.Where(tr => tr.UserId == userId).Include(r => r.TestResultRating);
        }

        public IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId)
        {
            return GetDBContext().TestResults.Where(tr => tr.TestId == testId && tr.UserId == userId);
        }

        public TestResult GetActiveTestResultByUser(int testId, int userId)
        {
            var now = DateTime.UtcNow;
            return GetDBContext().TestResults.FirstOrDefault(tr => 
                tr.TestId == testId && 
                tr.UserId == userId && 
                tr.TestResultStatus == TestResultStatus.InProgress && 
                (!tr.TestResultEndTime.HasValue || tr.TestResultEndTime > now)
            );
        }

        #endregion

        #region Test Repository
        
        public void AddTestAttribute(int testId, AttributeCode code, string value)
        {
            var db = GetDBContext();
            var attr = db.TestAttributes.FirstOrDefault(a => a.AttributeId == (int)code && a.TestId == testId);
            if (attr == null)
            {
                attr = new TestAttribute
                {
                    AttributeId = (int)code,
                    TestId = testId,
                    AttributeValue = value
                };
                db.TestAttributes.Add(attr);
            }
            else
            {
                attr.AttributeValue = value;
                Update(attr, false);
            }
            db.SaveChanges();
        }

        public void RemoveTestAttribute(int testId, AttributeCode code)
        {
            var db = GetDBContext();
            var attr = db.TestAttributes.FirstOrDefault(a => a.AttributeId == (int)code && a.TestId == testId);
            if (attr != null)
            {
                db.TestAttributes.Remove(attr);
                db.SaveChanges();
            }
            // TODO: set attribute value
            //attr.Value = questionTypeId.ToString();
            SaveChanges();
        }

        #endregion
    }
}
