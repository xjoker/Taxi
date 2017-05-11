using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.FileHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Taxi.StringHelper;
using System.Threading;

namespace Taxi.FileHelper.Tests
{
    [TestClass()]
    public class FileHelperTests
    {
        string a = "233";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "233Test.txt");

        [TestMethod()]
        public void ReadFileTest()
        {
            a.WriteToFile(path, true);
            var b=FileHelper.ReadFile(path);
            Assert.IsTrue(a == b);
        }

        [TestMethod()]
        public void ReadFileForAllTest()
        {
            FileHelper.WriteFile(path, a);
            var b = FileHelper.ReadFileForAll(path);
            b.Position = 0;
            var R = new StreamReader(b);
            string myStr = R.ReadToEnd();
            Assert.IsTrue(a == myStr);
        }

        [TestMethod()]
        public void WriteFileTest()
        {
            FileHelper.WriteFile(path, a);
            var b = FileHelper.ReadFile(path);
            Assert.IsTrue(a == b);
            Thread.Sleep(1000);
            FileHelper.WriteFile(path, "666",true);
            var c = FileHelper.ReadFile(path);
            Assert.IsTrue(a+"666" == c);

        }

    }
}