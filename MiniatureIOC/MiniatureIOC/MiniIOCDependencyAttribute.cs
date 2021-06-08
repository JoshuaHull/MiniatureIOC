using System;

namespace MiniatureIOC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MiniIOCDependencyAttribute: Attribute
    {
        public ServiceType ServiceType { get; private set; }

        public Type ConcreteClass { get; private set; }

        public Type InterfaceType { get; private set; }

        public MiniIOCDependencyAttribute(
            Type myInterface,
            ServiceType serviceType=MiniatureIOC.ServiceType.Transient
        ) {
            this.InterfaceType = myInterface;
            this.ServiceType = serviceType;
        }

        public MiniIOCDependencyAttribute(
            Type myInterface,
            Type concreteClass,
            ServiceType serviceType=MiniatureIOC.ServiceType.Transient
        ) {
            this.ConcreteClass = concreteClass;
            this.InterfaceType = myInterface;
            this.ServiceType = serviceType;
        }
    }
}
