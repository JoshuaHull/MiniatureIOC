using System;
using System.Linq;
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
          attribute.ServiceType.InvokeServiceMethod(
            services, attribute.InterfaceType, attribute.ConcreteClass ?? typeRegistration.concreteType
          );

      return services;
    }
  }
}
