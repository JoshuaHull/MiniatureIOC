using System;

namespace MiniatureIOC.Test.SingletonTestClasses
{
    [MiniIOCDependency(typeof(DDuffBeer), typeof(IBeer), ServiceType.Singleton)]
    public class DDuffBeer : IBeer
    {
        public DateTime CreatedTime { get; private set; }

        public DDuffBeer()
        {
            this.CreatedTime = DateTime.UtcNow;
        }
    }
}
