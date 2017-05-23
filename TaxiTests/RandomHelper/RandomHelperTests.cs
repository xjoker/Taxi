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
        public void GenerateRandomPasswordTest()
        {
            var b = RandomHelper.GenerateRandomPassword(10,includeNumber:false,includeMixedCase:false,includePunctuation:false);
            Assert.IsNotNull(b);
        }


    }
}