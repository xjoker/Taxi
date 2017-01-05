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
    public class AESEncryptsAndDecryptsTests
    {
        [TestMethod()]
        public void IVToBytesTest()
        {
            AESEncryptsAndDecrypts.IVToBytes("zxcvbnmdfrasdfgh");
            var b = new byte[] { 122, 120, 99, 118, 98, 110, 109, 100, 102, 114, 97, 115, 100, 102, 103, 104 };
            var c = AESEncryptsAndDecrypts.IVbytes;
            Assert.IsTrue(b.SequenceEqual(c));
        }

        [TestMethod()]
        public void EncryptTest()
        {
            AESEncryptsAndDecrypts.IVToBytes("zxcvbnmdfrasdfgh");
            var b = AESEncryptsAndDecrypts.Encrypt("TestEncrypt", "dofkrfaosrdedofkrfaosrdedofkrfao");
            string check = "MXViaOyMmTi78y3YHZFMZA==";
            Assert.AreEqual(b, check);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            AESEncryptsAndDecrypts.IVToBytes("zxcvbnmdfrasdfgh");
            var b = AESEncryptsAndDecrypts.Decrypt("MXViaOyMmTi78y3YHZFMZA==", "dofkrfaosrdedofkrfaosrdedofkrfao");
            string check = "TestEncrypt";
            Assert.AreEqual(b, check);
        }
    }
}