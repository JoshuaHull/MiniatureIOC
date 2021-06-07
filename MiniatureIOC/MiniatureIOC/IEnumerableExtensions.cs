using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiniatureIOC
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<Attribute>(
            this IEnumerable<Assembly> assemblies
        ) =>
            assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(Attribute), true).Length > 0)
                .Select(t => t);
    }
}
