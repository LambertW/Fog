using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FogDemo.Core.People
{
    public class Person : IEntity
    {
        public const int MaxNameLength = 32;

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }
        public Guid Id { get; set; }

        public Person()
        {

        }

        public Person(string name)
        {
            Name = name;
        }
    }
}
