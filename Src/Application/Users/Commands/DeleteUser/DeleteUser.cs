using Ardalis.GuardClauses;
using MediatR;
using Shortener.Application.Common.Interfaces;

namespace Shortener.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler(IAppDbContext context) : IRequestHandler<DeleteUserCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task Handle(DeleteUserCommand req, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync([req.Id], cancellationToken);

        Guard.Against.NotFound(req.Id, entity);

        _context.Users.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
};
