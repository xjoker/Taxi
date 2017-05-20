using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Network.Tests
{
    [TestClass()]
    public class NetworkToolsTests
    {
        [TestMethod()]
        public void PingCheckDetailedTest()
        {
            var b = NetworkTools.PingCheckDetailed("xjoker.us");
            Assert.IsFalse(b==new PingResponseType());
        }

        [TestMethod()]
        public void PingTest()
        {
            var b = NetworkTools.Ping("xjoker.us");
            Assert.IsFalse(b == null);
        }

        [TestMethod()]
        public void PingCheckTest()
        {
            var b = NetworkTools.PingCheck("baidu.com");
            Assert.IsFalse(b == false);
        }

        [TestMethod()]
        public void PingDelayTest()
        {
            var b = NetworkTools.PingDelay("baidu.com");
            Assert.IsFalse(b == null);
        }

        [TestMethod()]
        public void GetLocalIPsTest()
        {
            var b = NetworkTools.GetLocalIPs();
            Assert.IsFalse(b == null);
        }

        [TestMethod()]
        public void GetLocalMacsTest()
        {
            var b = NetworkTools.GetLocalMacs();
            Assert.IsFalse(b == null);
        }

        [TestMethod()]
        public void GetAllUsePortTest()
        {
            var b = NetworkTools.GetAllUsePort();
            Assert.IsFalse(b == null);
        }
    }
}