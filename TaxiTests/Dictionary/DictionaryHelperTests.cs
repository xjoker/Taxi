using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Dictionary.Tests
{
    [TestClass()]
    public class DictionaryHelperTests
    {
        private Dictionary<int, string> a = new Dictionary<int, string>() { [1] = "q", [2] = "w" };
        private Dictionary<int, string> b = new Dictionary<int, string>() { [3] = "z", [4] = "x" };
        private Dictionary<int, string> c = new Dictionary<int, string>() { [1] = "z", [4] = "x" };

        private Dictionary<int, string> d = new Dictionary<int, string>() { [1] = "q", [2] = "w", [3] = "z", [4] = "x" };
        private Dictionary<int, string> e = new Dictionary<int, string>() { [1] = "q", [2] = "w", [4] = "x" };

        [TestMethod()]
        public void MergeDictionaryAddTest()
        {
            Assert.IsTrue(DictionaryHelper.MergeDictionaryAdd(a, b).SequenceEqual(d));
        }

        [TestMethod()]
        public void MergeDictionaryReplaceTest()
        {
            Assert.IsTrue(DictionaryHelper.MergeDictionaryReplace(a, c).SequenceEqual(e));
        }

        [TestMethod()]
        public void GetValueTest()
        {
            var aa = a.GetValue(1);
            Assert.IsTrue(aa == "q");
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            var aa = a.AddRange(b, true);
            Assert.IsTrue(aa.SequenceEqual(d));
        }
    }
}