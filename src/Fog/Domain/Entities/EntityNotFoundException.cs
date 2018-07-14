using System;
using System.Collections.Generic;
using System.Text;

namespace Fog.Domain.Entities
{
    public class EntityNotFoundException : FogException
    {
        public Type EntityType { get; set; }

        public object Id { get; set; }

        public EntityNotFoundException() { }

        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {
        }

        public EntityNotFoundException(Type entityType, object id, Exception innerException)
        : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }
    }
}
