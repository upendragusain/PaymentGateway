using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PaymentGateway.Domain
{
    /*
     * Assumptions: 
     * The aim is just to persist the encrypted CARD data into db to be PCI compliant
     * To achieve this a simple (non-production ready) solution is to use AES encryption with a symmetric key
     * Also assuming that we won't be sharing the key as encrytions here is just for PCI compliance
     * If Howeever we were to send encrypted data over wire(https) to the bank for processing then we will have to think about
     * - encrypting the symmetric key by using asymmetric encryption.
     * As sending the key across an insecure network without encrypting it is unsafe, because anyone who intercepts the 
     * - key and IV can then decrypt the data.
     * Also the hardcoded key below should ideally be encrypted with another Admin key and saved to DB.
     * Wonder if it would be good strategy to have multiple keys based on merchant?
     */
    public class AESEncryptionService : IEncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _IV;

        public AESEncryptionService()
        {
            _key = HexToByteArray("892C8E496E1E33355E858B327BC238A939B7601E96159F9E9CAD0519BA5055CD");
            _IV = Encoding.UTF8.GetBytes("1234567890123456");
        }

        public byte[] Encrypt(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public string Decrypt(byte[] cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private byte[] HexToByteArray(string hexString)
        {
            if (0 != (hexString.Length % 2))
            {
                throw new ApplicationException("Hex string must be multiple of 2 in length");
            }

            int byteCount = hexString.Length / 2;
            byte[] byteValues = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                byteValues[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return byteValues;
        }
    }
}
