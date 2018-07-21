using Fog.Domain.Entities;
using FogDemo.Core.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FogDemo.Core.Tasks
{
    public class Task : IEntity<int>
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        [ForeignKey(nameof(AssignedPersonId))]
        public Person AssignedPerson { get; set; }
        public Guid? AssignedPersonId { get; set; }
        public int Id { get; set; }

        public Task()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Open;
        }

        public Task(string title, string description = null, Guid? assignedPersonId = null)
            : this()
        {
            Title = title;
            Description = description;
            AssignedPersonId = assignedPersonId;
        }
    }
}
