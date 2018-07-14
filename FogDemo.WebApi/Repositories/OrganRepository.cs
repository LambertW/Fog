using Fog.Domain.Repositories;
using FogDemo.Core.Domain;
using FogDemo.WebApi.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FogDemo.WebApi.Repositories
{
    public class OrganRepository : TreeRepositoryBase<Organ, Guid>, IOrganRepository
    {
        public readonly MyDbContext MyDbContext;

        public virtual DbSet<Organ> Table => MyDbContext.Organs;

        public OrganRepository(MyDbContext dbContext)
        {
            MyDbContext = dbContext;

            dbContext.Database.EnsureCreated();
        }

        public override async Task<Organ> InsertAsync(Organ entity, Organ parentEntity)
        {
            entity.InitPath(parentEntity);

            var result = await Table.AddAsync(entity);
            return result.Entity;
        }

        public override async Task<Organ> InsertAsync(Organ entity)
        {
            entity.InitPath();

            var result = await Table.AddAsync(entity);
            return result.Entity;
        }

        public override Task<int> GenerateSortIdAsync(Guid parentId)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Organ>> GetAllChildrenAsync(Organ parent)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(Organ entity)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<Organ> GetAll()
        {
            return Table.AsQueryable();
        }

        public override async Task<List<Organ>> GetAllListAsync()
        {
            return await Table.ToListAsync();
        }

        public override Task<Organ> UpdateAsync(Organ entity)
        {
            throw new NotImplementedException();
        }
    }
}
