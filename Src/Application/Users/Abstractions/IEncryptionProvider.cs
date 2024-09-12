namespace Shortener.Application.Users.Abstractions
{
    public interface IEncryptionProvider
    {
        string Hash(string str);
        bool Verify(string str, string hash);
    }
}
