using Shortener.Domain.Entities;

namespace Shortener.Application.Users.Abstractions;
public interface IJwtProvider
{
    string Generate(User user);
}
