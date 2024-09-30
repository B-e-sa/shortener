
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Common.Models;
using Shortener.Domain.Common.Exceptions;

namespace Shortener.Application.Common;

public class TokenService(
    IJwtProvider jwtProvider,
    IAppDbContext dbContext) : ITokenService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly IAppDbContext _dbContext = dbContext;

    public async Task<User?> GetUser(string token, CancellationToken cancellationToken)
    {
        if (!_jwtProvider.Validate(token))
        {
            throw new InvalidTokenException();
        }

        var userId = _jwtProvider.Read(token).Id;

        var foundUser = await _dbContext
            .Users
            .FindAsync([userId], cancellationToken)
            ?? throw new InvalidTokenException();

        return foundUser;
    }
}
