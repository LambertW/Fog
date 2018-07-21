using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Repositories;
using FogDemo.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public class OrganRepository : EfCoreTreeRepositoryBase<Organ>, IOrganRepository
    {
        public OrganRepository(IUnitOfWork unitOfWork, MyDbContext dbContext)
            : base(unitOfWork, dbContext)
        {
        }
    }
}
