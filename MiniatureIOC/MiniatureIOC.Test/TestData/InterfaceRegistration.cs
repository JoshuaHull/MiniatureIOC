namespace MiniatureIOC.Test.TestData
{
    public interface IInterfaceRegistration { }

    [MiniIOCDependency(typeof(IInterfaceRegistration))]
    public class InterfaceRegistration : IInterfaceRegistration { }
}
