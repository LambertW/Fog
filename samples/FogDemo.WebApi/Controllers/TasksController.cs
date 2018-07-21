using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FogDemo.Application.Tasks;
using FogDemo.Application.Tasks.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FogDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        [HttpPost("GetAll")]
        public async Task<List<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            return await _taskAppService.GetAll(input);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateTaskInput input)
        {
            await _taskAppService.Create(input);

            return new OkResult();
        }
    }
}
