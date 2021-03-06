using NUnit.Framework;
using System.Security.Cryptography;

namespace PaymentGateway.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string original = "Here is some data to encrypt!";

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {

                // Encrypt the string to an array of bytes.
                byte[] encrypted = Aes_Example.EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                // Decrypt the bytes to a string.
                string roundtrip = Aes_Example.DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                Assert.IsTrue(true);
            }
        }
    }
}