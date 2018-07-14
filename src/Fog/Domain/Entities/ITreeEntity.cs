using System;

namespace Fog.Domain.Entities
{
    public interface ITreeEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        void InitPath();

        void InitPath(TreeEntityBase<TPrimaryKey> parent);
    }

    public interface ITreeEntity : ITreeEntity<Guid>
    {
    }
}