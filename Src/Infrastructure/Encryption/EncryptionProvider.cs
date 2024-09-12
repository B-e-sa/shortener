using Shortener.Application.Users.Abstractions;
using Shortener.Domain.Entities;

namespace Shortener.Infrastructure.Encryption
{
    public sealed class EncryptionProvider : IEncryptionProvider
    {
        public string Hash(string str)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            return BCrypt.Net.BCrypt.HashPassword(str, salt);
        }
    }
}
