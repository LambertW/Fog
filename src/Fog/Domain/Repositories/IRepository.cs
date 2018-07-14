using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query

        IQueryable<TEntity> GetAll();

        Task<List<TEntity>> GetAllListAsync();

        Task<TEntity> GetAsync(TPrimaryKey id);

        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);


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
