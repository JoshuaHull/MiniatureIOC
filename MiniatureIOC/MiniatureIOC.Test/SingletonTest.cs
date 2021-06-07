using MiniatureIOC.Test.SingletonTestClasses;
using NUnit.Framework;
using System.Threading;

namespace MiniatureIOC.Test
{
    [TestFixture]
    public class SingletonTest
    {
        public SingletonTest()
        {

        }

        [Test]
        public void Can_Find_And_Load_Singleton_Dependencies()
        {
            RHomer homer1 = new RHomer();
            RHomer homer2 = new RHomer();

            var iocHandler = new MiniIOCHandler(assemblies: typeof(SingletonTest).Assembly);

            iocHandler.Load().Build().ResolveFor(homer1);
            Thread.Sleep(1000);
            iocHandler.ResolveFor(homer2);

            Assert.IsNotNull(homer1.Beer);
            Assert.IsNotNull(homer2.Beer);
            Assert.IsInstanceOf(typeof(DDuffBeer), homer1.Beer);
            Assert.IsInstanceOf(typeof(DDuffBeer), homer2.Beer);
            Assert.AreEqual(((DDuffBeer)homer1.Beer).CreatedTime, ((DDuffBeer)homer2.Beer).CreatedTime);
        }
    }
}
