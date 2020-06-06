using System;

namespace MiniatureIOC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MiniIOCDependencyAttribute: Attribute
    {
        public ServiceSubType ServiceType { get; private set; }

        public Type ConcreteClass { get; private set; }

        public Type InterfaceType { get; private set; }

        public MiniIOCDependencyAttribute(
            Type concreteClass,
            ServiceType serviceType=MiniatureIOC.ServiceType.Transient
        ) {
            this.ConcreteClass = concreteClass;
            this.ServiceType = serviceType.ToSubType(1);
        }

        public MiniIOCDependencyAttribute(
            Type concreteClass, Type myInterface,
            ServiceType serviceType=MiniatureIOC.ServiceType.Transient
        ) {
            this.ConcreteClass = concreteClass;
            this.InterfaceType = myInterface;
            this.ServiceType = serviceType.ToSubType(2);
        }
    }
}
