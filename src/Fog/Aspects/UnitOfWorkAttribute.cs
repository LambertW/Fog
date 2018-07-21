using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Fog.Domain.Uow;
using AspectCore.Extensions.AspectScope;

namespace Fog.Aspects
{
    public class UnitOfWorkAttribute : InterceptorBaseAttribute, IScopeInterceptor
    {
        public Scope Scope { get; set; } = Scope.Aspect;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);

            var unitOfWork = context.ServiceProvider.GetService<IUnitOfWork>();

            if (unitOfWork == null)
                return;

            await unitOfWork.CommitAsync();

            //if (context.Implementation is ICommitAfter service)
            //    service.CommitAfter();
        }
    }
}
