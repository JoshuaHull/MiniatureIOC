using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MiniatureIOC
{
    public enum Lifetime
    {
        Transient,
        Scoped,
        Singleton,
    }

    static class LifetimeExtensions
    {
        public static string GetAddServiceMethodName(this Lifetime serviceType) => serviceType switch
        {
            Lifetime.Transient => nameof(ServiceCollectionServiceExtensions.AddTransient),
            Lifetime.Scoped => nameof(ServiceCollectionServiceExtensions.AddScoped),
            Lifetime.Singleton => nameof(ServiceCollectionServiceExtensions.AddSingleton),
            _ => throw new NotImplementedException(),
        };

        public static MethodInfo? GetServiceMethod(this Lifetime serviceType)
            => typeof(ServiceCollectionServiceExtensions).GetMethod(
                serviceType.GetAddServiceMethodName(),
                new[] {typeof(IServiceCollection), typeof(Type), typeof(Type)}
            );

        public static object? InvokeServiceMethod(
            this Lifetime lifetime,
            IServiceCollection services,
            Type serviceType,
            Type concreteType
        ) {
            return lifetime.GetServiceMethod()?.Invoke(
                services, new object[] { services, serviceType, concreteType }
            );
        }
    }
}
