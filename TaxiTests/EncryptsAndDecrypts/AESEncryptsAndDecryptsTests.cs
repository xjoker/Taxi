using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Taxi.EncryptsAndDecrypts.Tests
{
    [TestClass()]
    public class AESEncryptsAndDecryptsTests
    {
        [TestMethod()]
        public void EncryptTest()
        {
            using (Aes myAes = Aes.Create())
            {
                var b = AESEncryptsAndDecrypts.Encrypt("TestEncrypt", "dofkrfaosrdedofkrfaosrdedofkrfao", myAes.IV);
                var c = AESEncryptsAndDecrypts.Decrypt(b, "dofkrfaosrdedofkrfaosrdedofkrfao", myAes.IV);
                Assert.IsTrue(c=="TestEncrypt");
            }
        }

        [TestMethod()]
        public void SimpleEncryptTest()
        {
            var b = AESEncryptsAndDecrypts.SimpleEncrypt("TestEncrypt", "123456");
            var c = AESEncryptsAndDecrypts.SimpleDecrypt(b, "123456");
            Assert.IsTrue(c=="TestEncrypt");
        }
    }
}