using FogDemo.Application.Tasks.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fog.Linq.Extensions;
using FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories.Tasks;
using Fog.Domain.Uow;
using Fog.Aspects;

namespace FogDemo.Application.Tasks
{
    public class TaskAppService : ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskAppService(ITaskRepository repository)
        {
            _taskRepository = repository;
        }

        public virtual async System.Threading.Tasks.Task Create(CreateTaskInput input)
        {
            var task = new Core.Tasks.Task
            {
                AssignedPersonId = input.AssignedPersonId,
                Description = input.Description,
                Title = input.Title 
            };

            await _taskRepository.InsertAsync(task);
        }

        public virtual async Task<List<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t => t.AssignedPerson)
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                //.OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            var dtoList = new List<TaskListDto>();
            tasks.ForEach(t => dtoList.Add(new TaskListDto
            {
                AssignedPersonId = t.AssignedPersonId,
                AssignedPersonName = t.AssignedPerson?.Name,
                CreationTime = t.CreationTime,
                Description = t.Description,
                State = t.State,
                Title = t.Title
            }));

            return dtoList;
        }
    }
}
