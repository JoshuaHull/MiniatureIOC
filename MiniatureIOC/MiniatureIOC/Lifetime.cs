using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MiniatureIOC
{
    public enum Lifetime
    {
        Transient,
        Singleton,
    }

    static class LifetimeExtensions
    {
        public static string GetAddServiceMethod(this Lifetime serviceType) => serviceType switch
        {
            Lifetime.Transient => nameof(ServiceCollectionServiceExtensions.AddTransient),
            Lifetime.Singleton => nameof(ServiceCollectionServiceExtensions.AddSingleton),
            _ => throw new NotImplementedException(),
        };

        public static MethodInfo? GetServiceMethod(this Lifetime serviceType)
            => typeof(ServiceCollectionServiceExtensions).GetMethod(
                serviceType.GetAddServiceMethod(),
                new[] {typeof(IServiceCollection), typeof(Type), typeof(Type)}
            );

        public static object? InvokeServiceMethod(
            this Lifetime serviceType,
            IServiceCollection services,
            Type interfaceType,
            Type concreteClass
        ) {
            return serviceType.GetServiceMethod()?.Invoke(
                services, new object[] { services, interfaceType, concreteClass }
            );
        }
    }
}
