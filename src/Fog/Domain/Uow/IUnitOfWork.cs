using System;
using System.Threading.Tasks;

namespace Fog.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        Task CommitAsync();
    }
}
