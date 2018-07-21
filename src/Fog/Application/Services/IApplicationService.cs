using Fog.Aspects;
using Fog.Dependency;

namespace Fog.Application.Services
{
    [UnitOfWork(Inherited = true)]
    public interface IApplicationService : ITransientDependency
    {
    }
}
