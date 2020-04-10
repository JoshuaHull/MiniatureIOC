using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MiniatureIOC.Helpers
{
    public static class Enumerators
    {
        public static List<Type> GetTypesFromAllAssembliesMatchingRegex(string regex)
        {
            var allTypes = new List<Type>();

            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();
            IEnumerable<AssemblyName> assemblyNames = GetReferencedAssemblyNames(assemblies);
            IEnumerable<Type> typesFromAssemblies = GetTypesFromAssembliesMatchingRegex(assemblies, regex);
            IEnumerable<Type> typesFromNames = GetTypesFromAssemblyNamesMatchingRegex(assemblyNames, regex);

            allTypes.AddRange(typesFromAssemblies);
            allTypes.AddRange(typesFromNames);

            return allTypes;
        }

        public static IEnumerable<AssemblyName> GetReferencedAssemblyNames(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
                foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
                    yield return assemblyName;
        }

        public static IEnumerable<Type> GetTypesFromAssemblyNamesMatchingRegex(
            IEnumerable<AssemblyName> assemblyNames, string regex
        ) {
            foreach (AssemblyName assemblyName in assemblyNames)
                if (RegexHelp.IsMatch(regex, assemblyName.FullName))
                    foreach (var type in GetTypesFromAssemblyName(assemblyName))
                        yield return type;
        }

        public static IEnumerable<Type> GetTypesFromAssembliesMatchingRegex(
            IEnumerable<Assembly> assemblies, string regex
        ) {
            foreach (Assembly assembly in assemblies)
                if (RegexHelp.IsMatch(regex, assembly.FullName))
                    foreach (var type in assembly.GetTypes())
                        yield return type;
        }

        public static List<Type> GetTypesFromAssemblyName(AssemblyName assemblyName)
        {
            var types = new List<Type>();
            Assembly assembly;

            try {
                assembly = Assembly.Load(assemblyName);
            } catch (Exception e) {
                throw new Exception($"Failed to load assembly: {assemblyName}.", e);
            }

            foreach (Type t in assembly.GetTypes())
                types.Add(t);

            return types;
        }

        public static List<Type> GetTypesWithAttribute<T>(IEnumerable<Type> types)
            where T : class
        {
            var typesWithAttribute = new List<Type>();

            foreach (var type in types) {
                try {
                    if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                        typesWithAttribute.Add(type);
                } catch { }
            }

            return typesWithAttribute;
        }
    }
}
