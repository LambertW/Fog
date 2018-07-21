using Fog.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories.Tasks
{
    public interface ITaskRepository : IRepository<FogDemo.Core.Tasks.Task, int>
    {
        Task CommitAsync();
    }
}
