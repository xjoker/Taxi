using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taxi.EnumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Dictionary;

namespace Taxi.EnumHelper.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        enum TestEnum
        {
            [System.ComponentModel.Description("AAA")]
            A = 1,
            [System.ComponentModel.Description("BBB")]
            B = 2,
            [System.ComponentModel.Description("CCC")]
            C = 3
        }


        [TestMethod()]
        public void ToEnumTest()
        {
            var b=EnumHelper.ToEnum<TestEnum>("A");
            Assert.IsTrue(b==TestEnum.A);
        }

        [TestMethod()]
        public void ToEnumNameTest()
        {
            var b = new List<string>{"A","B","C"};
            var d = EnumHelper.ToEnumName<TestEnum>();
            Assert.IsTrue(b == d);
        }

        [TestMethod()]
        public void GetIntValueTest()
        {
            var b = TestEnum.B.GetIntValue();
            Assert.IsTrue(b == 2);
        }

        [TestMethod()]
        public void GetDescriptionTest()
        {
            var b = TestEnum.B.GetDescription();
            Assert.IsTrue(b == "BBB");
        }

        [TestMethod()]
        public void ToDictionaryTest()
        {
            var d = new Dictionary<int, string>()
            {
                {1,"AAA" },
                {2,"BBB" },
                {3,"CCC" }
            };
            var b = EnumHelper.ToDictionary<TestEnum>();
            Assert.IsTrue(d.DictionaryEqual(b));
        }

    }
}