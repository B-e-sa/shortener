using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Users;

namespace Shortener.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(string Token) : IRequest;

public class DeleteUserCommandHandler(
    IAppDbContext context,
    ITokenService tokenService
    ) : IRequestHandler<DeleteUserCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task Handle(DeleteUserCommand req, CancellationToken cancellationToken)
    {
        var foundUser = await _tokenService.GetUser(req.Token, cancellationToken);

        _context.Users.Remove(foundUser);
        await _context.SaveChangesAsync(cancellationToken);
    }
};
