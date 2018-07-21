using Autofac;
using Fog.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fog.Dependency
{
    internal class DependencyConfiguration
    {
        private IServiceCollection _services;
        private IConfig[] _configs;

        private ContainerBuilder _builder;
        private IFinder _finder;

        private List<Assembly> _assemblies;

        public DependencyConfiguration(IServiceCollection services, IConfig[] configs)
        {
            _services = services;
            _configs = configs;
        }

        public IServiceProvider Config()
        {
            return IocManager.Instance.Register(_services, RegisterServices, _configs);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            _builder = builder;
            _finder = new WebFinder();
            _assemblies = _finder.GetAssemblies();

            //RegisterInfrastracture();
            //RegisterEventHandlers();
            RegisterDependency();
        }

        private void RegisterDependency()
        {
            _builder.RegisterTypes(GetTypes<ISingletonDependency>())
                .AsImplementedInterfaces().PropertiesAutowired().SingleInstance();

            _builder.RegisterTypes(GetTypes<IScopeDependency>())
                .AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();

            _builder.RegisterTypes(GetTypes<ITransientDependency>())
                .AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
        }

        private Type[] GetTypes<T>()
        {
            return _finder.Find<T>(_assemblies).ToArray();
        }
    }
};