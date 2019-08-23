using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core = TimecardsCore.Interfaces;
using ioc = TimecardsIOC;

namespace TimecardsTesting.IntegrationTests
{
    [TestClass]
    public class IOCTests
    {
        private ioc.Factory _factory = null;
        private HashSet<string> _randomStrings = null;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new ioc.Factory();
            _randomStrings = CollectionOfUniqueRandomStrings(10);
        }

        [TestMethod]
        public void RegisterResolveTest()
        {
            // register a singleton
            _factory.Register<IAlpha>(typeof(Alpha), true);

            // register a class that is built from that singleton
            _factory.Register<IBeta>(typeof(Beta), false, typeof(IAlpha));

            // get a reference to the factory
            core.IFactory ifactory = _factory;

            // get a singleton
            var a1 = ifactory.Resolve<IAlpha>();
            Assert.IsTrue(a1 is IAlpha, "Did not receive singleton object");
            var a2 = ifactory.Resolve<IAlpha>();
            Assert.AreEqual(a1, a2, "Second resolution of singleton resulted in a different object");
            Assert.AreEqual(a1.Concat(), "12", "Singleton has unexpected behavior");

            // manufacture some objects
            var b1 = ifactory.Resolve<IBeta>();
            var b2 = ifactory.Resolve<IBeta>();
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

        private HashSet<string> CollectionOfUniqueRandomStrings(int quantity)
        {
            var random = new Random();
            var collection = new HashSet<string>();

            while (collection.Count < quantity)
                collection.Add(random.Next(10000, 99999).ToString());

            return collection;
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
            private static readonly HashSet<string> previousStrings = new HashSet<string>();
            private static readonly Random random = new Random();

            public string First { get; private set; }
            public string Second { get; private set; }
            public string Third { get; private set; }

            public Beta(IAlpha alpha)
            {
                First = alpha.First;
                Second = alpha.Second;

                // assign a guaranteed unique string value to Third
                do
                {
                    Third = random.Next(1000, 9999).ToString();
                } while (previousStrings.Contains(Third));

                previousStrings.Add(Third);
            }

            public string Flip() => Second + First;
        }
    }
}
