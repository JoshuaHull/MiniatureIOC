using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MiniatureIOC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMiniIOCDependenciesFromAssembliesContaining(
            this IServiceCollection services, params Type[] types
        ) {
            if (types == null) return services;
            if (types.Length == 0) return services;

            var typeRegistrations = types
            .Select(t => t.Assembly)
            .GetTypesWithAttribute<MiniIOCDependencyAttribute>()
            .Select(concreteType => new {
                concreteType,
                attributes = concreteType.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true),
            });

            foreach (var typeRegistration in typeRegistrations)
            foreach (MiniIOCDependencyAttribute attribute in typeRegistration.attributes)
                attribute.Lifetime.InvokeServiceMethod(
                services,
                attribute.ServiceType ?? typeRegistration.concreteType,
                attribute.ConcreteType ?? typeRegistration.concreteType
                );

            return services;
        }
    }

    internal static class IEnumerableExtensions
    {
        public static IEnumerable<Type> GetTypesWithAttribute<Attribute>(
            this IEnumerable<Assembly> assemblies
        ) =>
            assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(Attribute), true).Length > 0);
    }
}
