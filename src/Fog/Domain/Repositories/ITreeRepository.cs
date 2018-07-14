using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Repositories
{
    public interface ITreeRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        Task<int> GenerateSortIdAsync(TPrimaryKey parentId);

        Task<List<TEntity>> GetAllChildrenAsync(TEntity parent);

        Task<TEntity> InsertAsync(TEntity entity, TEntity parentEntity);
    }

    public interface ITreeRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
    }
}
