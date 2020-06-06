using System;

namespace MiniatureIOC.Test.TransientTestClasses
{
    [MiniIOCDependency(typeof(DDuffBeer), typeof(IBeer), ServiceType.Transient)]
    public class DDuffBeer : IBeer
    {
        public DateTime CreatedTime { get; private set;  }

        public DDuffBeer()
        {
            this.CreatedTime = DateTime.UtcNow;
        }
    }
}
