using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;

namespace EduTesting.EntityFramework.Repositories
{
    public abstract class EduTestingRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<EduTestingDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public abstract class EduTestingRepositoryBase<TEntity> : EduTestingRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {

    }
}
