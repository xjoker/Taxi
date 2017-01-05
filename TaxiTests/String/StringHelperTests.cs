using Taxi.StringHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Taxi.StringHelper.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void WriteToFileTest()
        {
            string a = "233";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "233Test.txt");
            a.WriteToFile(path, true);
            bool b = File.Exists(path);
            Assert.IsTrue(b);
        }

        [TestMethod()]
        public void IsValidJsonTest()
        {
            string a = "{\"username\":\"salt\",\"password\":\"salt@pass\",\"eauth\":\"pam\"}";
            bool b = a.IsValidJson();
            Assert.IsTrue(b);
        }

        [TestMethod()]
        public void ToIntTest()
        {
            string a = "1";
            int b = 1;
            Assert.IsTrue(a.ToInt() == b);
        }

        [TestMethod()]
        public void IsIntTest()
        {
            string a = "1";
            Assert.IsTrue(a.IsInt());
        }


        [TestMethod()]
        public void IsNullOrEmptyTest()
        {
            string a = null;
            Assert.IsTrue(a.IsNullOrEmpty());
        }

        [TestMethod()]
        public void IsNullOrWhiteSpaceTest()
        {
            string a = "";
            Assert.IsTrue(a.IsNullOrWhiteSpace());
        }
    }
}