using Microsoft.Extensions.DependencyInjection;
using MiniatureIOC.Helpers;
using System;
using System.Collections.Generic;

namespace MiniatureIOC
{
    public class MiniIOCHandler
    {
        private string AssemblyRegex { get; set; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        internal List<Type> AllTypes { get; set; }

        public MiniIOCHandler(string assemblyRegex, IServiceCollection initialServices=null)
        {
            this.AssemblyRegex = assemblyRegex ??
                throw new ArgumentException("Assembly regex should not be null. Pass \".*\" so search over all assemblies.");
            this.Services = initialServices ?? new ServiceCollection();
        }

        #region VIRTUAL METHODS

        public virtual void LoadDependencies()
        {
            this.AllTypes = Enumerators.GetTypesFromAllAssembliesMatchingRegex(this.AssemblyRegex);

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

        public void ResolveServicesFor(object resolveOn)
        {
            var properties = resolveOn.GetType().GetProperties();

            foreach (var property in properties) {
                var pType = property.PropertyType;

                try {
                    var service = this.ServiceProvider.GetService(pType);
                    property.SetValue(resolveOn, service);
                } catch { }
            }
        }

        #region PRIVATE METHODS

        private List<Type> GetTypesWithMiniIOCDependencyAttribute()
            => Enumerators.GetTypesWithAttribute<MiniIOCDependencyAttribute>(this.AllTypes);

        #endregion
    }
}
