using NUnit.Framework;
using PaymentGateway.API.Application;
using PaymentGateway.Domain;

namespace PaymentGateway.Test
{
    public class AESEncryptionServiceShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EncyrptAndDecryptData()
        {
            string original = "Here is some data to encrypt!";

            var encryptionService = new AESEncryptionService();

            var encrypted = encryptionService.Encrypt(original);
            var decrypted = encryptionService.Decrypt(encrypted);
            Assert.AreEqual(original, decrypted);
        }
    }

}