using Microsoft.Extensions.DependencyInjection;
using MiniatureIOC.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiniatureIOC
{
    public class MiniIOCHandler
    {
        private string AssemblyRegex { get; set; }

        private string NamespaceRegex { get; set; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        internal IEnumerable<Type> AllTypes { get; set; }

        public MiniIOCHandler(string assemblyRegex=null, string namespaceRegex=null, IServiceCollection initialServices=null)
        {
            this.AssemblyRegex = assemblyRegex ?? ".*";
            this.NamespaceRegex = namespaceRegex ?? ".*";
            this.Services = initialServices ?? new ServiceCollection();
        }

        #region VIRTUAL METHODS

        public virtual void LoadDependencies()
        {
            this.AllTypes = Enumerators.GetTypesMatchingNamespaceRegex(
                Enumerators.GetTypesFromAllAssembliesMatchingRegex(this.AssemblyRegex),
                this.NamespaceRegex
            );

            this.LoadDependencies(this.GetTypesWithMiniIOCDependencyAttribute());
        }

        public virtual void BuildServiceProvider()
            => this.ServiceProvider = this.Services.BuildServiceProvider();

        #endregion

        public void LoadDependencies(List<Type> types)
        {
            foreach (var type in types)
                foreach (MiniIOCDependencyAttribute dependency in type.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true))
                    dependency.ServiceType.InvokeServiceMethod(this.Services, dependency.InterfaceType, dependency.ConcreteClass);
        }

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

        #region PRIVATE METHODS

        private List<Type> GetTypesWithMiniIOCDependencyAttribute()
            => Enumerators.GetTypesWithAttribute<MiniIOCDependencyAttribute>(this.AllTypes);

        #endregion
    }
}
