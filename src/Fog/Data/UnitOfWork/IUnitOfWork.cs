using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();

        Task<int> CommitAsync();
    }
}
