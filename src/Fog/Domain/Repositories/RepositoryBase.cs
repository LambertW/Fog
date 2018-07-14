using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(GetAll().Count());
        }
        public abstract Task DeleteAsync(TEntity entity);

        public abstract Task DeleteAsync(TPrimaryKey id);

        public abstract IQueryable<TEntity> GetAll();

        public abstract Task<List<TEntity>> GetAllListAsync();

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            var entity = GetAll().FirstOrDefault(t => t.Id.Equals(id));
            return entity;
        }

        public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity), id);

            return entity;
        }

        public abstract Task<TEntity> InsertAsync(TEntity entity);

        public abstract Task<TEntity> UpdateAsync(TEntity entity);

    }
}
