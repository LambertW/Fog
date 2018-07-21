using Fog.Domain.Repositories;

namespace FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories.Tasks
{
    public interface ITaskRepository : IRepository<FogDemo.Core.Tasks.Task, int>
    {
    }
}
