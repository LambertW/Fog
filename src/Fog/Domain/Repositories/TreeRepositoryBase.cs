using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Repositories
{
    public abstract class TreeRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : TreeEntityBase<TPrimaryKey>
    {
        public abstract Task<int> GenerateSortIdAsync(TPrimaryKey parentId);


        public abstract Task<TEntity> InsertAsync(TEntity entity, TEntity parentEntity);

        public abstract Task<List<TEntity>> GetAllChildrenAsync(TEntity parent);
    }
}
