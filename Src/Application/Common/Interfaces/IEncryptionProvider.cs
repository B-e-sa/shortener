namespace Shortener.Application.Common.Interfaces
{
    public interface IEncryptionProvider
    {
        string Hash(string str);
        bool Verify(string str, string hash);
    }
}
