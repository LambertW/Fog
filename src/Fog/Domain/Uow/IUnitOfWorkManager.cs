using Fog.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Uow
{
    public interface IUnitOfWorkManager : IScopeDependency
    {
        void Commit();

        Task CommitAsync();

        void Register(IUnitOfWork unitOfWork);
    }
}
