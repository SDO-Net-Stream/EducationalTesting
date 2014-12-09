using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EduTesting.Model;

namespace EduTesting.Repository
{
	public interface IRepository : IDisposable
	{
		IQueryable<T> SelectAll<T>(params Expression<Func<T, object>>[] includeObjects) where T : class;
	}

	public class Repository : IRepository
	{
		#region Constructor_Dispose

		public Repository()
		{
			DBContext = new EduTestingContext();
		}

		public void Dispose()
		{
			if (DBContext != null)
			{
				DBContext.Dispose();
			}
		}

		#endregion

		public IQueryable<T> SelectAll<T>(params Expression<Func<T, object>>[] includeObjects) where T : class
		{
			IQueryable<T> resultList = DBContext.Set<T>();

			foreach (var includeItem in includeObjects)
			{
				resultList = resultList.Include(includeItem);
			}

			return resultList;
		}

		void Update<T>(IQueryable<T> items) where T : class
		{
		}

		void Delete()
		{
		}

		void Insert()
		{
		}

		#region Members

		protected EduTestingContext DBContext { get; private set; }

		#endregion
	}
}
