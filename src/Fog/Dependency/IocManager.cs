using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fog.Dependency
{
    public class IocManager
    {
        public static IocManager Instance { get; private set; }

        public IContainer IocContainer { get; private set; }

        static IocManager()
        {
            Instance = new IocManager();
        }

        private IocManager()
        {
        }

        public IServiceProvider Register(IServiceCollection services, Action<ContainerBuilder> actionBefore, params IConfig[] configs)
        {
            var builder = new ContainerBuilder();

            actionBefore?.Invoke(builder);

            foreach (var config in configs)
            {
                builder.RegisterModule(config);
            }

            builder.RegisterInstance(IocManager.Instance).AsSelf().SingleInstance();

            if (services != null)
                builder.Populate(services);

            IocContainer = builder.Build();

            return new AutofacServiceProvider(IocContainer);
        }

        //public T Resolve<T>()
        //{
        //    return IocContainer.Resolve<T>();
        //}

        //public T Resolve<T>(Type type)
        //{
        //    return (T)IocContainer.BeginLifetimeScope().Resolve(type);
        //}
    }
}
