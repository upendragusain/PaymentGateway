namespace PaymentGateway.Domain
{
    public interface IEncryptionService
    {
        string Decrypt(byte[] cipherText);
        byte[] Encrypt(string plainText);
    }
}