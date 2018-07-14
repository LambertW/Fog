using System;
using System.Collections.Generic;
using System.Text;

namespace Fog.Domain.Entities
{
    public abstract class TreeEntityBase<TPrimaryKey> : IEntity<TPrimaryKey>, ITreeEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        public string Name { get; set; }

        public TPrimaryKey ParentId { get; set; }

        public string ParentName { get; set; }

        public string Path { get; private set; }

        public int Level { get; private set; }

        public TreeEntityBase(TPrimaryKey id)
        {
            Id = id;
        }

        public virtual void InitPath()
        {
            InitPath(default(TreeEntityBase<TPrimaryKey>));
        }

        public virtual void InitPath(TreeEntityBase<TPrimaryKey> parent)
        {
            if(Equals(parent, null))
            {
                Level = 1;
                Path = $"{Id},";
                return;
            }

            ParentId = parent.Id;
            ParentName = parent.Name;
            Level = parent.Level + 1;
            Path = $"{parent.Path}{Id},";
        }
    }

    public abstract class TreeEntityBase : TreeEntityBase<Guid>
    {
        public TreeEntityBase(Guid id) : base(id)
        {
        }
    }
}
