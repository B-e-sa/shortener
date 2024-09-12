using Shortener.Application.Users.Abstractions;

namespace Shortener.Infrastructure.Encryption
{
    public sealed class EncryptionProvider : IEncryptionProvider
    {
        public string Hash(string str)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(str, salt);
        }
        public bool Verify(string str, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(str, hash);
        }

    }
}
