using System;

namespace MiniatureIOC.Test.TestData
{
    [MiniIOCDependency]
    public class BasicRegistration
    {
        public DateTime CreatedOn { get; }

        public BasicRegistration() {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
