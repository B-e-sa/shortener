using Shortener.Application.Common.Models;

namespace Shortener.Application.Common.Interfaces;

public interface ITokenService
{
    Task<User?> GetUser(string token, CancellationToken cancellationToken);
}