using System;
using System.Collections.Generic;
using System.Text;

namespace Fog.Domain.Entities
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {
    }
}
