using FogDemo.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace FogDemo.Application.Tasks.Dtos
{
    public class GetAllTasksInput
    {
        public TaskState? State { get; set; }
    }
}
