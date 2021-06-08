using System;

namespace MiniatureIOC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MiniIOCDependencyAttribute: Attribute
    {
        public Lifetime Lifetime { get; private set; }

        public Type? ConcreteType { get; private set; }

        public Type? ServiceType { get; private set; }

        public MiniIOCDependencyAttribute(
            Lifetime lifetime = Lifetime.Transient
        ) {
            Lifetime = lifetime;
        }

        public MiniIOCDependencyAttribute(
            Type serviceType,
            Lifetime lifetime = Lifetime.Transient
        ) {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public MiniIOCDependencyAttribute(
            Type serviceType,
            Type concreteType,
            Lifetime lifetime = Lifetime.Transient
        ) {
            ConcreteType = concreteType;
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
    }
}
