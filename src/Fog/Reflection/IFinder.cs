using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fog.Reflection
{
    public interface IFinder
    {
        List<Assembly> GetAssemblies();

        List<Type> Find<T>(List<Assembly> assemblies = null);

        List<Type> Find(Type type, List<Assembly> assemblies = null);
    }
}
