using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.Array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Array.Tests
{
    [TestClass()]
    public class ArrayHelperTests
    {
        [TestMethod()]
        public void JoinToStringTest()
        {
            int[] a = new int[5] { 1,2,3,4,5};
            var b=a.JoinToString(",");
            Assert.IsTrue(b == "1,2,3,4,5");
        }
    }
}