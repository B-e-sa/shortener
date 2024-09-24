using Shortener.Application.Common.Models;

namespace Shortener.Application.Common.Interfaces;

public interface ITokenService
{
    ClaimDTO GetPayload(string token);
}