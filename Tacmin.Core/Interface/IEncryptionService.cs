namespace Tacmin.Core.Interface
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText);

        string Encrypt(string plainText, string key);

        string Decrypt(string plainText);

        string Decrypt(string plainText, string key);
    }
}
