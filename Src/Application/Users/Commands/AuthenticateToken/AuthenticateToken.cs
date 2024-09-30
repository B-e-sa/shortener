using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Common.Models;

namespace Shortener.Application.Users.Commands.AuthenticateToken;

public record AuthenticateTokenCommand(string Token) : IRequest<UserDTO>;

public class AuthenticateTokenCommandHandler(
    IAppDbContext context,
    ITokenService tokenService) : IRequestHandler<AuthenticateTokenCommand, UserDTO>
{
    private readonly ITokenService _tokenService = tokenService;

    public async Task<UserDTO> Handle(AuthenticateTokenCommand req, CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        UserDTO userDto = new()
        {
            Id = foundUser.Id,
            UserName = foundUser.Username,
            Email = foundUser.Email,
            ConfirmedEmail = foundUser.ConfirmedEmail
        };

        return userDto;
    }
}