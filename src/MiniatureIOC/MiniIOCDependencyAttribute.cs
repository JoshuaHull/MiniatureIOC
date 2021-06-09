using System;

namespace MiniatureIOC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MiniIOCDependencyAttribute: Attribute
    {
        public Lifetime Lifetime { get; }

        public Type? ConcreteType { get; }

        public Type? ServiceType { get; }

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
            ServiceType = serviceType;
            ConcreteType = concreteType;
            Lifetime = lifetime;
        }
    }
}
