using System;

namespace MiniatureIOC.Test.TestData
{
    [MiniIOCDependency(Lifetime.Singleton)]
    public class SingletonRegistration
    {
        public DateTime CreatedOn { get; }

        public SingletonRegistration() {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
