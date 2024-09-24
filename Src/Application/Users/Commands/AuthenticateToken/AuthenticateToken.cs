using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Application.Common.Models;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.AuthenticateToken;

public record AuthenticateTokenCommand(string Token) : IRequest<UserDTO>;

public class AuthenticateTokenCommandHandler(
    IAppDbContext context,
    ITokenService tokenService
    ) : IRequestHandler<AuthenticateTokenCommand, UserDTO>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<UserDTO> Handle(AuthenticateTokenCommand req, CancellationToken cancellationToken)
    {
        var payload = _tokenService.GetPayload(req.Token);

        var foundUser = await _context.Users
             .Where(u => u.Id == payload.Id)
             .Select(u => new UserDTO
             {
                 Id = u.Id,
                 UserName = u.Username,
                 Email = u.Email,
                 ConfirmedEmail = u.ConfirmedEmail
             })
             .FirstOrDefaultAsync(cancellationToken)
             ?? throw new UserNotFoundException();

        return foundUser;
    }
}