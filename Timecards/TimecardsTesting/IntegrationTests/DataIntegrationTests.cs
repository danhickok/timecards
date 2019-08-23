using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ci = TimecardsCore.Interfaces;
using ioc = TimecardsIOC;

namespace TimecardsTesting.IntegrationTests
{
    [TestClass]
    public class DataIntegrationTests
    {
        private ioc.Factory _factory = null;

        [TestInitialize]
        public void Initialize()
        {
            _factory = new ioc.Factory();
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
    }
}
