using Fog.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Fog
{
    public static class Extensions
    {
        public static IServiceProvider ConfigAndReturnServiceProvider(this IServiceCollection services, params IConfig[] configs)
        {
            //services.AddHttpContextAccessor();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return new DependencyConfiguration(services, configs).Config();
        }
    }
}
