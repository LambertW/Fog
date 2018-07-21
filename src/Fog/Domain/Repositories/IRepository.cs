using Fog.Dependency;
using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fog.Domain.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> : IScopeDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query

        IQueryable<TEntity> GetAll();

        Task<List<TEntity>> GetAllListAsync();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TPrimaryKey id);

        Task<TEntity> GetAsync(TPrimaryKey id);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Insert

        Task<TEntity> InsertAsync(TEntity entity);
        #endregion

        #region Update
        Task<TEntity> UpdateAsync(TEntity entity);
        #endregion

        #region Delete

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(TPrimaryKey id);
        #endregion

        #region Aggregates

        Task<int> CountAsync();
        #endregion
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {

    }
}
