using Microsoft.VisualStudio.TestTools.UnitTesting;
using ci = TimecardsCore.Interfaces;
using ioc = TimecardsIOC;

namespace TimecardsTesting.IntegrationTests
{
    [TestClass]
    public class IOCTests
    {
        private ioc.Factory _factory = null;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new ioc.Factory();
        }

        [TestMethod]
        public void RegisterResolveTest()
        {
            // register a singleton
            _factory.Register<IAlpha>(typeof(Alpha), true);

            // register a class that is built from that singleton
            _factory.Register<IBeta>(typeof(Beta), false, typeof(IAlpha));

            // get a reference to the factory
            ci.IFactory factory = _factory;

            // get a singleton
            var a1 = factory.Resolve<IAlpha>();
            Assert.IsTrue(a1 is IAlpha, "Did not receive singleton object");
            var a2 = factory.Resolve<IAlpha>();
            Assert.AreEqual(a1, a2, "Second resolution of singleton resulted in a different object");
            Assert.AreEqual(a1.Concat(), "12", "Singleton has unexpected behavior");

            // manufacture some objects
            var b1 = factory.Resolve<IBeta>();
            var b2 = factory.Resolve<IBeta>();
            Assert.AreNotEqual(b1, b2, "Received same object for non-singleton type");
            Assert.AreEqual(b1.Flip(), "21", "Non-singleton #1 has unexpected behavior");
            Assert.AreEqual(b2.Flip(), "21", "Non-singleton #2 has unexpected behavior");
            Assert.AreNotEqual(b1.Third, b2.Third, "Non-singletons have non-unique property values");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_factory != null)
            {
                _factory.Dispose();
                _factory = null;
            }
        }

        //
        // classes and interfaces for testing register/resolve methods
        //

        private interface IAlpha
        {
            string First { get; }
            string Second { get; }
            string Concat();
        }

        private class Alpha : IAlpha
        {
            public string First { get; private set; }
            public string Second { get; private set; }

            public Alpha()
            {
                First = "1";
                Second = "2";
            }

            public string Concat() => First + Second;
        }

        private interface IBeta
        {
            string Third { get; }
            string Flip();
        }

        private class Beta : IBeta
        {
            private static int InstanceCounter = 0;

            private string _first;
            public string _second;

            public string Third { get; private set; }

            public Beta(IAlpha alpha)
            {
                _first = alpha.First;
                _second = alpha.Second;
                Third = (++InstanceCounter).ToString();
            }

            public string Flip() => _second + _first;
        }
    }
}
