
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Common.Models;
using Shortener.Domain.Common.Exceptions;

namespace Shortener.Application.Common;

public class TokenService(IJwtProvider jwtProvider) : ITokenService
{
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public ClaimDTO GetPayload(string token)
    {
        if (!_jwtProvider.Validate(token))
        {
            throw new InvalidTokenException();
        }

        return _jwtProvider.Read(token);
    }
}
