using Fog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fog.Tests.Domain.Entities
{
    public class Organ : TreeEntityBase
    {
        public Organ(Guid id) : base(id)
        {
        }

        public int? SortNo { get; set; }

        public int Status { get; set; }
    }
}
