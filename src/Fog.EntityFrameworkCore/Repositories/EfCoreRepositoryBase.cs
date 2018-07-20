using Fog.Domain.Entities;
using Fog.Domain.Repositories;
using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fog.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContext _context;

        public virtual DbSet<TEntity> Table => _context.Set<TEntity>();

        public EfCoreRepositoryBase(IUnitOfWork unitOfWork, DbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = ((EfCoreUnitOfWork)_unitOfWork).GetOrCreateDbContext(context);
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
            return Table.AsQueryable();
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await Table.AddAsync(entity);
            return entry.Entity;
        }

        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entity != null)
                return;

            Table.Attach(entity);
        }

        protected TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                        );

            return entry?.Entity as TEntity;
        }
    }

    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
        public EfCoreRepositoryBase(IUnitOfWork unitOfWork, DbContext context) : base(unitOfWork, context)
        {
        }
    }
}
