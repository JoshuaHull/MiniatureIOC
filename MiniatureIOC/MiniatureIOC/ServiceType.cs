using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiniatureIOC
{
    public enum ServiceType
    {
        Transient,
        Singleton,
    }

    static class ServiceSubTypeExtensions
    {
        public static string GetValue(this ServiceType serviceType)
        {
            switch (serviceType) {
                case ServiceType.Transient:
                    return nameof(ServiceCollectionServiceExtensions.AddTransient);
                case ServiceType.Singleton:
                    return nameof(ServiceCollectionServiceExtensions.AddSingleton);
            }

            throw new NotImplementedException();
        }

        public static MethodInfo GetServiceMethod(this ServiceType serviceType)
            => typeof(ServiceCollectionServiceExtensions).GetMethod(
                serviceType.GetValue(),
                new[] {typeof(IServiceCollection), typeof(Type), typeof(Type)}
            );

        public static object InvokeServiceMethod(
            this ServiceType serviceType,
            IServiceCollection services,
            Type interfaceType,
            Type concreteClass
        ) {
            return serviceType.GetServiceMethod().Invoke(
                services, new object[] { services, interfaceType, concreteClass }
            );
        }
    }
}
