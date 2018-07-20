using Fog.Domain.Entities;
using Fog.Domain.Repositories;
using Fog.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.EntityFrameworkCore.Repositories
{
    public class EfCoreTreeRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<TEntity, TPrimaryKey>, ITreeRepository<TEntity, TPrimaryKey> where TEntity : TreeEntityBase<TPrimaryKey>
    {
        public EfCoreTreeRepositoryBase(IUnitOfWork unitOfWork, DbContext context) 
            : base(unitOfWork, context)
        {
        }

        public virtual Task<int> GenerateSortIdAsync(TPrimaryKey parentId)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<TEntity>> GetAllChildrenAsync(TEntity parent)
        {
            var entityList = GetAll().Where(t => t.Path.Contains(parent.Id.ToString()));
            return Task.FromResult(entityList.ToList());
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, TEntity parentEntity)
        {
            entity.InitPath(parentEntity);

            return await InsertAsync(entity);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, TPrimaryKey parentId)
        {
            var parentEntity = await GetAsync(parentId);

            return await InsertAsync(entity, parentEntity);
        }
    }

    public class EfCoreTreeRepositoryBase<TEntity> : EfCoreTreeRepositoryBase<TEntity, Guid>, ITreeRepository<TEntity, Guid> where TEntity : TreeEntityBase<Guid>
    {
        public EfCoreTreeRepositoryBase(IUnitOfWork unitOfWork, DbContext context) : base(unitOfWork, context)
        {
        }
    }
}
