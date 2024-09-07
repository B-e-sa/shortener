using Ardalis.GuardClauses;
using MediatR;
using Shortener.Application.Common.Interfaces;

namespace Shortener.Application.Urls.Commands.DeleteUrl;

public record DeleteUrlCommand(int Id) : IRequest;

public class DeleteUrlCommandHandler(IAppDbContext context) : IRequestHandler<DeleteUrlCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task Handle(DeleteUrlCommand req, CancellationToken cancellationToken)
    {
        var entity = await _context.Urls
            .FindAsync([req.Id], cancellationToken);

        Guard.Against.NotFound(req.Id, entity);

        _context.Urls.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
};
