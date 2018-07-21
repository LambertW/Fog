using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Fog.Reflection
{
    public class Finder : IFinder
    {
        private const string SkipAssemblies = "^System|^Mscorlib|^Netstandard|^Microsoft|^Autofac|^AutoMapper|^EntityFramework|^Newtonsoft|^Castle|^NLog|^Pomelo|^AspectCore|^Xunit|^Nito|^Npgsql|^Exceptionless|^MySqlConnector|^Anonymously Hosted|^libuv|^api-ms|^clrcompression|^clretwrc|^clrjit|^coreclr|^dbgshim|^e_sqlite3|^hostfxr|^hostpolicy|^MessagePack|^mscordaccore|^mscordbi|^mscorrc|sni|sos|SOS.NETCore|^sos_amd64|^SQLitePCLRaw|^StackExchange|^Swashbuckle|WindowsBase|ucrtbase";

        public List<Type> Find<T>(List<Assembly> assemblies = null)
        {
            return Find(typeof(T), assemblies);
        }

        public List<Type> Find(Type type, List<Assembly> assemblies = null)
        {
            assemblies = assemblies ?? GetAssemblies();
            var result = new List<Type>();
            foreach (var assembly in assemblies)
            {
                result.AddRange(GetTypes(type, assembly));
            }

            return result.Distinct().ToList();
        }

        private IEnumerable<Type> GetTypes(Type findType, Assembly assembly)
        {
            var result = new List<Type>();
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }

            if (types == null)
                return result;

            foreach (var type in types)
            {
                AddType(result, findType, type);
            }
            return result;
        }

        private void AddType(List<Type> result, Type findType, Type type)
        {
            if (type.IsInterface || type.IsAbstract)
                return;

            if (!findType.IsAssignableFrom(type) && !MatchGeneric(findType, type))
                return;

            result.Add(type);
        }

        private bool MatchGeneric(Type findType, Type type)
        {
            if (!findType.IsGenericTypeDefinition)
                return false;

            var definition = findType.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (!implementedInterface.IsGenericType)
                    continue;

                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }

        public virtual List<Assembly> GetAssemblies()
        {
            return GetAssembliesFromCurrentDomain();
        }

        private List<Assembly> GetAssembliesFromCurrentDomain()
        {
            var result = new List<Assembly>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Match(assembly))
                    result.Add(assembly);
            }

            return result.Distinct().ToList();
        }

        private bool Match(Assembly assembly)
        {
            return !Regex.IsMatch(assembly.FullName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        protected void LoadAssemblies(string path)
        {
            foreach (var file in Directory.GetFiles(path, "*.dll"))
            {
                if (!Match(Path.GetFileName(file)))
                {
                    continue;
                }

                var assemblyName = AssemblyName.GetAssemblyName(file);
                AppDomain.CurrentDomain.Load(assemblyName);
            }
        }

        protected virtual bool Match(string assemblyName)
        {
            if (assemblyName.StartsWith($"{Assembly.GetEntryAssembly().GetName().Name}.Views"))
                return false;

            if (assemblyName.StartsWith($"{Assembly.GetEntryAssembly().GetName().Name}.PrecompiledViews"))
                return false;

            return !Regex.IsMatch(assemblyName, SkipAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
