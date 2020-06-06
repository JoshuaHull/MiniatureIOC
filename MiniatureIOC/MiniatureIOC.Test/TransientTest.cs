using MiniatureIOC.Test.TransientTestClasses;
using NUnit.Framework;
using System.Threading;

namespace MiniatureIOC.Test
{
    [TestFixture]
    public class TransientTest
    {
        [Test]
        public void Can_Find_And_Load_Transient_Dependencies()
        {
            RHomer homer1 = new RHomer();
            RHomer homer2 = new RHomer();

            var iocHandler = new MiniIOCHandler("MiniatureIOC.Test");

            iocHandler.Load().Build().ResolveFor(homer1);
            Thread.Sleep(5000);
            iocHandler.ResolveFor(homer2);

            Assert.IsNotNull(homer1.Beer);
            Assert.IsNotNull(homer2.Beer);
            Assert.IsInstanceOf(typeof(DDuffBeer), homer1.Beer);
            Assert.IsInstanceOf(typeof(DDuffBeer), homer2.Beer);
            Assert.Greater(((DDuffBeer)homer2.Beer).CreatedTime, ((DDuffBeer)homer1.Beer).CreatedTime);
        }
    }
}
