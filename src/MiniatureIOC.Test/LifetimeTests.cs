using Microsoft.Extensions.DependencyInjection;
using MiniatureIOC.Test.TestData;
using NUnit.Framework;
using Shouldly;

namespace MiniatureIOC.Test.Extensions
{
    public class LifetimeExtensionsTests
    {
        [Test]
        public void Should_Get_AddTransient_Method_Name_From_Transient_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Transient;

            // Act
            var name = lifetime.GetAddServiceMethodName();

            // Assert
            name.ShouldBe("AddTransient");
        }

        [Test]
        public void Should_Get_AddScoped_Method_Name_From_Scoped_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Scoped;

            // Act
            var name = lifetime.GetAddServiceMethodName();

            // Assert
            name.ShouldBe("AddScoped");
        }

        [Test]
        public void Should_Get_AddSingleton_Method_Name_From_Singleton_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Singleton;

            // Act
            var name = lifetime.GetAddServiceMethodName();

            // Assert
            name.ShouldBe("AddSingleton");
        }

        [Test]
        public void Should_Get_AddTransient_Method_From_Transient_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Transient;

            // Act
            var method = lifetime.GetServiceMethod();

            // Assert
            method.Name.ShouldBe("AddTransient");
            method.GetParameters().Length.ShouldBe(3);
        }

        [Test]
        public void Should_Get_AddScoped_Method_From_Scoped_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Scoped;

            // Act
            var method = lifetime.GetServiceMethod();

            // Assert
            method.Name.ShouldBe("AddScoped");
            method.GetParameters().Length.ShouldBe(3);
        }

        [Test]
        public void Should_Get_AddSingleton_Method_From_Singleton_Lifetime()
        {
            // Arrange
            var lifetime = Lifetime.Singleton;

            // Act
            var method = lifetime.GetServiceMethod();

            // Assert
            method.Name.ShouldBe("AddSingleton");
            method.GetParameters().Length.ShouldBe(3);
        }

        [Test]
        public void Should_Add_Type_To_ServiceCollection()
        {
            // Arrange
            var lifetime = Lifetime.Transient;
            var services = new ServiceCollection();

            // Act
            lifetime.InvokeServiceMethod(services, typeof(BasicRegistration), typeof(BasicRegistration));

            // Assert
            services.Count.ShouldBe(1);
        }
    }
}
