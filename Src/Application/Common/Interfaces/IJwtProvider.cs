using Shortener.Application.Common.Models;

namespace Shortener.Application.Common.Interfaces;

public interface IJwtProvider
{
    string Generate(User user);
    ClaimDTO Read(string token);
    bool Validate(string token);
}
