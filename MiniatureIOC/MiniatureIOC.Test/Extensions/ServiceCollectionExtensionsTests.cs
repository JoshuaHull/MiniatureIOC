using Microsoft.Extensions.DependencyInjection;
using MiniatureIOC.Extensions;
using MiniatureIOC.Test.TestData;
using NUnit.Framework;
using Shouldly;

namespace MiniatureIOC.Test.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        private ServiceCollection ServiceCollection;

        [SetUp]
        public void SetUp()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddMiniIOCDependenciesFromAssembliesContaining(typeof(BasicRegistration));
        }

        [Test]
        public void Should_Add_All_Types_From_Assembly_With_Attribute()
        {
            // Assert
            ServiceCollection.Count.ShouldBe(3);
        }

        [Test]
        public void Should_Register_BasicRegistration_As_Itself()
        {
            // Arrange
            var sp = ServiceCollection.BuildServiceProvider();

            // Act
            var basicRegistrationInstance = sp.GetService<BasicRegistration>();

            // Assert
            basicRegistrationInstance.ShouldNotBeNull();
        }

        [Test]
        public void Should_Register_InterfaceRegistration_As_Interface()
        {
            // Arrange
            var sp = ServiceCollection.BuildServiceProvider();

            // Act
            var interfaceRegistrationInstance = sp.GetService<IInterfaceRegistration>();

            // Assert
            interfaceRegistrationInstance.ShouldNotBeNull();
        }

        [Test]
        public void Should_Not_Register_InterfaceRegistration_As_Itself()
        {
            // Arrange
            var sp = ServiceCollection.BuildServiceProvider();

            // Act
            var interfaceRegistrationInstance = sp.GetService<InterfaceRegistration>();

            // Assert
            interfaceRegistrationInstance.ShouldBeNull();
        }

        [Test]
        public void Should_Register_SingletonRegistration_As_Singleton()
        {
            // Arrange
            var sp = ServiceCollection.BuildServiceProvider();

            // Act
            var first = sp.GetService<SingletonRegistration>();
            var second = sp.GetService<SingletonRegistration>();

            // Assert
            first.CreatedOn.ShouldBe(second.CreatedOn);
        }
    }
}
