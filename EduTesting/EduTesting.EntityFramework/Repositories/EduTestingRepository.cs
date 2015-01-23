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

        public IEnumerable<Question> GetQuestionsByTest(int testId)
        {
            return GetDBContext().Questions.Where(q => q.TestId == testId);
        }

        public IEnumerable<TestResult> GetTestResultsByTest(int testId)
        {
            return GetDBContext().TestResults.Where(tr => tr.TestId == testId);
        }

        public IEnumerable<TestResult> GetTestResultsByUser(int userId)
        {
            return GetDBContext().TestResults.Where(tr => tr.UserId == userId);
        }

        public IEnumerable<TestResult> GetTestResultsByTestAndUser(int testId, int userId)
        {
            return GetDBContext().TestResults.Where(tr => tr.TestId == testId && tr.UserId == userId);
        }

        public TestResult GetActiveTestResultByUser(int testId, int userId)
        {
            return GetDBContext().TestResults.FirstOrDefault(tr => tr.TestId == testId && tr.UserId == userId && tr.IsCompleted == false);
        }

        public IEnumerable<UserAnswer> GetUserAnswersByTestResultId(int testsResultId)
        {
            return GetDBContext().UsersAnswers.Where(ua => ua.TestResult.TestResultId == testsResultId);
        }

        public void UpdateQuestionType(int questionId, int questionTypeId)
        {
            var db = GetDBContext();
            var attr = db.QuestionAttributes.FirstOrDefault(qa => qa.QuestionID == questionId && qa.AttributeID == EduTestingConsts.AttributeId_QuestionType);
            if (attr == null)
            {
                attr = new QuestionAttribute
                {
                    AttributeID = EduTestingConsts.AttributeId_QuestionType,
                    QuestionID = questionId
                };
                db.QuestionAttributes.Add(attr);
            }
            attr.Value = questionTypeId.ToString();
            db.SaveChanges();
        }

        public void UpdateAnswerIsRight(int answerId, bool isRight)
        {
            var answer = SelectById<Answer>(answerId);
            if (answer.Attributes == null)
                answer.Attributes = new List<CustomAttribute>();
            var attr = answer.Attributes.FirstOrDefault(qa => qa.AttributeID == EduTestingConsts.AttributeId_AnswerIsRight);
            if (attr == null)
            {
                attr = SelectById<CustomAttribute>(EduTestingConsts.AttributeId_AnswerIsRight);
                answer.Attributes.Add(attr);
            }
            // TODO: set attribute value
            //attr.Value = questionTypeId.ToString();
            SaveChanges();
        }

        #endregion


    }
}
