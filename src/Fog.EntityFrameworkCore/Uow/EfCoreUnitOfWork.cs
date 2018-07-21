using Autofac;
using Fog.Dependency;
using Fog.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Fog.EntityFrameworkCore.Uow
{
    public class EfCoreUnitOfWork : IUnitOfWork, IScopeDependency
    {
        protected IDictionary<string, DbContext> ActiveDbContexts { get; } = new Dictionary<string, DbContext>();

        private readonly IocManager _iocManager;

        public EfCoreUnitOfWork(IocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public void Commit()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }

        public async Task CommitAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        public virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>(TDbContext dbContext) where TDbContext : DbContext
        {
            // TODO Change DbContextKey
            var dbContextKey = typeof(TDbContext).FullName;
            //DbContext dbContext;
            //if(!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            //{
            //    dbContext = _iocManager.IocContainer.Resolve<TDbContext>();
            //}
            //else
            //{
                
            //}

            ActiveDbContexts[dbContextKey] = dbContext;

            return (TDbContext)dbContext;
        }

        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }

        public void Dispose()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                dbContext.Dispose();
            }
        }
    }
}
