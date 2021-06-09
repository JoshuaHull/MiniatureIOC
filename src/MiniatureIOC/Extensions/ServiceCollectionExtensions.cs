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
                .Select(Assembly)
                .SelectMany(Types)
                .Where(HaveMiniIOCAttribute)
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

        private static Func<Type, Assembly> Assembly =>
            type => type.Assembly;

        private static Func<Assembly, IEnumerable<Type>> Types =>
            assembly => assembly.GetTypes();

        private static Func<Type, bool> HaveMiniIOCAttribute =>
            type => type.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true).Length > 0;
    }
}
