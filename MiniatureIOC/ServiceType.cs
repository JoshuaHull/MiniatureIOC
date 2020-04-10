using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiniatureIOC
{
    public enum ServiceType
    {
        Transient,
    }

    public enum ServiceSubType
    {
        Transient1 = 1,
        Transient2 = 2,
    }

    static class ServiceTypeExtensions
    {
        public static ServiceSubType ToSubType(this ServiceType serviceType, int paramCount)
        {
            string sTypeName = Enum.GetName(typeof(ServiceType), serviceType);
            string subTypeName = sTypeName + paramCount;

            return (ServiceSubType)Enum.Parse(typeof(ServiceSubType), subTypeName);
        }
    }

    static class ServiceSubTypeExtensions
    {
        public static string GetValue(this ServiceSubType serviceSubType)
        {
            switch (serviceSubType) {
                case ServiceSubType.Transient1:
                case ServiceSubType.Transient2:
                    return nameof(ServiceCollectionServiceExtensions.AddTransient);
            }

            throw new NotImplementedException();
        }

        public static int GetParamCount(this ServiceSubType serviceSubType)
            => (int)serviceSubType;

        public static MethodInfo GetServiceMethod(this ServiceSubType serviceType)
            => typeof(ServiceCollectionServiceExtensions).GetMethod(serviceType.GetValue(), serviceType.GetParamArray());

        public static object InvokeServiceMethod(this ServiceSubType serviceType, IServiceCollection services)
        {
            if (serviceType.GetParamCount() != 0)
                throw new ArgumentException();

            return serviceType.GetServiceMethod().Invoke(
                services, new[] { services }
            );
        }

        public static object InvokeServiceMethod(this ServiceSubType serviceType,
            IServiceCollection services, Type concreteClass=null)
        {
            if (concreteClass == null)
                return serviceType.InvokeServiceMethod(services);

            if (serviceType.GetParamCount() != 1)
                throw new ArgumentException();

            return serviceType.GetServiceMethod().Invoke(
                services, new object[] { services, concreteClass }
            );
        }

        public static object InvokeServiceMethod(this ServiceSubType serviceType,
            IServiceCollection services, Type interfaceType=null, Type concreteClass=null)
        {
            if (interfaceType == null)
                return serviceType.InvokeServiceMethod(services, concreteClass);

            if (serviceType.GetParamCount() != 2)
                throw new ArgumentException();

            return serviceType.GetServiceMethod().Invoke(
                services, new object[] { services, interfaceType, concreteClass }
            );
        }

        private static Type[] GetParamArray(this ServiceSubType serviceType)
        {
            var paramTypes = new List<Type> { typeof(IServiceCollection) };

            for (int i = 0; i < serviceType.GetParamCount(); i += 1)
                paramTypes.Add(typeof(Type));

            return paramTypes.ToArray();
        }
    }
}
