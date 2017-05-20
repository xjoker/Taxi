using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Taxi.StringHelper;

namespace Taxi.DateTimeHelper.Tests
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        private string a = "1483346453";
        private DateTime dt = Convert.ToDateTime("2017/1/2 16:40:53");

        [TestMethod()]
        public void StampToDateTimeTest()
        {
            var c = a.StampToDateTime();
            Assert.IsTrue(dt == c);
        }

        [TestMethod()]
        public void DateTimeToStampTest()
        {
            
            int c = dt.DateTimeToStamp();
            Assert.IsTrue(a.ToInt()==c);
        }

        [TestMethod()]
        public void FirstDayOfMonthTest()
        {
            var aa = DateTimeHelper.FirstDayOfMonth(dt);
            DateTime bb = Convert.ToDateTime("2017/1/1 16:40:53");
            Assert.IsTrue(aa == bb);
        }

        [TestMethod()]
        public void LastDayOfMonthTest()
        {
            var aa = DateTimeHelper.LastDayOfMonth(dt);
            DateTime bb = Convert.ToDateTime("2017/1/31 16:40:53");
            Assert.IsTrue(aa == bb);
        }

        [TestMethod()]
        public void FirstDayOfPreviousMonthTest()
        {
            var aa = DateTimeHelper.FirstDayOfPreviousMonth(dt);
            DateTime bb = Convert.ToDateTime("2016/12/1 16:40:53");
            Assert.IsTrue(aa == bb);
        }

        [TestMethod()]
        public void LastDayOfPrdviousMonthTest()
        {
            var aa = DateTimeHelper.LastDayOfPrdviousMonth(dt);
            DateTime bb = Convert.ToDateTime("2016/12/31 16:40:53");
            Assert.IsTrue(aa == bb);
        }

        [TestMethod()]
        public void GetWeekDayTest()
        {
            var aa = new DateTime(2017,5,20);
            var dd= DateTimeHelper.GetWeekDayEnglish(aa);
            Assert.IsTrue("Saturday"==dd);
        }
    }
}