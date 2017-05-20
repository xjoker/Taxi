using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.SystemHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.SystemHelper.Tests
{
    [TestClass()]
    public class SystemHelperTests
    {
        [TestMethod()]
        public void Keybd_eventTest()
        {
            SystemHelper.Keybd_event(SystemHelper.VK_LWIN, 0, SystemHelper.KEYEVENTF_EXTENDEDKEY, 0);
            Assert.Fail();
        }
    }
}