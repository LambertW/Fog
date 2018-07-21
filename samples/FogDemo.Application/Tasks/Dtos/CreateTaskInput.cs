using FogDemo.Core.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FogDemo.Application.Tasks.Dtos
{
    public class CreateTaskInput
    {
        [Required]
        [MaxLength(Task.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(Task.MaxDescriptionLength)]
        public string Description { get; set; }

        public Guid? AssignedPersonId { get; set; }
    }
}
