using FogDemo.Core.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace FogDemo.Application.Tasks.Dtos
{
    public class TaskListDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public Guid? AssignedPersonId { get; set; }

        public string AssignedPersonName { get; set; }
    }
}
