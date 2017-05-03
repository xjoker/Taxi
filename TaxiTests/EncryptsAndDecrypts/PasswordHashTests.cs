using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.EncryptsAndDecrypts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.EncryptsAndDecrypts.Tests
{
    [TestClass()]
    public class PasswordHashTests
    {
        [TestMethod()]
        public void CreateHashTest()
        {
            var b = PasswordHash.CreateHash("123456");
            var c = PasswordHash.ValidatePassword("123456", b);
            Assert.IsTrue(c);
        }
    }
}