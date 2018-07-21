using Fog.Domain.Uow;
using Fog.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories.Tasks
{
    public class TaskRepository : EfCoreRepositoryBase<FogDemo.Core.Tasks.Task, int>, ITaskRepository
    {
        public TaskRepository(IUnitOfWork unitOfWork, MyDbContext context) : base(unitOfWork, context)
        {
        }
    }
}
