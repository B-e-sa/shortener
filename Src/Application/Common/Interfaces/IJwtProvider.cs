using Shortener.Domain.Entities;

namespace Shortener.Application.Common.Interfaces;

public class ClaimDto
{
    public int Id { get; set; }
    public string Email { get; set; }
}

public interface IJwtProvider
{
    string Generate(User user);
    ClaimDto Read(string token);
    bool Validate(string token);
}
