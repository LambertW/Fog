using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Repositories;
using FogDemo.Core.Domain;
using FogDemo.WebApi.Db;

namespace FogDemo.WebApi.Repositories
{
    public class OrganRepository : EfCoreTreeRepositoryBase<Organ>, IOrganRepository
    {
        public OrganRepository(IUnitOfWork unitOfWork, MyDbContext dbContext) 
            : base(unitOfWork, dbContext)
        {
        }
    }
}
