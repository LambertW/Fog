using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}
