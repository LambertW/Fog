using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Repositories;

namespace FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories.Tasks
{
    public class TaskRepository : EfCoreRepositoryBase<FogDemo.Core.Tasks.Task, int>, ITaskRepository
    {
        public TaskRepository(IUnitOfWork unitOfWork, MyDbContext context) : base(unitOfWork, context)
        {
        }
    }
}
