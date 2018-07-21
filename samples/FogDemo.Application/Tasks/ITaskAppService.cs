using Fog.Application.Services;
using Fog.Aspects;
using FogDemo.Application.Tasks.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FogDemo.Application.Tasks
{
    [UnitOfWork(Inherited = true)]
    public interface ITaskAppService : IApplicationService
    {
        Task<List<TaskListDto>> GetAll(GetAllTasksInput input);

        System.Threading.Tasks.Task Create(CreateTaskInput input);
    }
}
