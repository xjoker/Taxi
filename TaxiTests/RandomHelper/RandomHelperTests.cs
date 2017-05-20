using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.RandomHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.RandomHelper.Tests
{
    [TestClass()]
    public class RandomHelperTests
    {
        [TestMethod()]
        public void GenerateRandomNumberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateRandomStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateRandomStringTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateRandomPasswordTest()
        {
            var b = RandomHelper.GenerateRandomPassword(10,includeNumber:false,includeMixedCase:false,includePunctuation:false);
            Assert.IsNotNull(b);
        }

        [TestMethod()]
        public void GetRandomStringTest()
        {
            Assert.Fail();
        }
    }
}