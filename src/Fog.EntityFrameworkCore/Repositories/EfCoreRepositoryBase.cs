using Fog.Domain.Entities;
using Fog.Domain.Repositories;
using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fog.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext context;

        public virtual DbSet<TEntity> Table => context.Set<TEntity>();

        public EfCoreRepositoryBase(IUnitOfWork unitOfWork, DbContext context)
        {
            _unitOfWork = unitOfWork;
            ((EfCoreUnitOfWork)_unitOfWork).GetOrCreateDbContext(context);
        }

        public override Task DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            return Task.CompletedTask;
        }

        public override async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if(entity != null)
            {
                await DeleteAsync(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if(entity != null)
            {
                await DeleteAsync(entity);
                return;
            }
        }

        public override IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<List<TEntity>> GetAllListAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entity != null)
                return;

            Table.Attach(entity);
        }

        protected TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                        );

            return entry?.Entity as TEntity;
        }
    }
}
