﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Taxi.Log.Tests
{
    [TestClass()]
    public class LogTests
    {
        [TestMethod()]
        public void LogWriteTest()
        {
            LogHelper.Instance.LogWrite("233");
            DateTime now = DateTime.Now;
            Assert.IsTrue(File.Exists(Path.Combine(LogHelper.Instance.LogDirectory, LogHelper.Instance.FileNamePrefix+now.ToString("yyyyMMdd'.log'"))));
        }

    }
}