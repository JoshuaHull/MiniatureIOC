using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiniatureIOC
{
    public class MiniIOCHandler
    {
        private IEnumerable<Assembly> Assemblies { get; set; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        internal IEnumerable<Type> AllTypes { get; set; }

        public MiniIOCHandler(IServiceCollection initialServices = null, params Assembly[] assemblies)
        {
            this.Assemblies = assemblies ?? new Assembly[] { };
            this.Services = initialServices ?? new ServiceCollection();
        }

        public virtual void LoadDependencies()
        {
            this.AllTypes = this.Assemblies?.GetTypesWithAttribute<MiniIOCDependencyAttribute>();

            foreach (var t in this.AllTypes)
                foreach (MiniIOCDependencyAttribute d in t.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true))
                    d.ServiceType.InvokeServiceMethod(this.Services, d.InterfaceType, d.ConcreteClass);
        }

        public virtual void BuildServiceProvider()
            => this.ServiceProvider = this.Services.BuildServiceProvider();

        public virtual void ResolveServicesFor(object resolveOn)
        {
            var properties = resolveOn.GetType().GetProperties();

            foreach (var property in properties)
            {
                var pType = property.PropertyType;
                object service;

                try {
                    service = this.ServiceProvider.GetService(pType);
                } catch (Exception e) {
                    throw new Exception($"Dependency injection is set up incorrectly. Could not resolve service {pType}.", e);
                }

                if (service == null)
                    continue;

                try {
                    property.SetValue(resolveOn, service);
                } catch (Exception e) when (
                  e is ArgumentException || e is TargetException ||
                  e is TargetInvocationException || e is MethodAccessException
                ) {
                    continue;
                }
            }
        }
    }
}
