using Ardalis.GuardClauses;
using MediatR;
using Shortener.Application.Common.Interfaces;

namespace Shortener.Application.Urls.Queries.FindUrlById;

public sealed record FindUrlByIdQuery(int Id) : IRequest<Domain.Entities.Url>;

internal sealed class FindUrlByIdQueryHandler(IAppDbContext context) : IRequestHandler<FindUrlByIdQuery, Domain.Entities.Url>
{
    readonly IAppDbContext _context = context;

    public async Task<Domain.Entities.Url> Handle(
        FindUrlByIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUrl = await _context.Urls.FindAsync([req.Id], cancellationToken: cancellationToken);

        Guard.Against.NotFound(req.Id, foundUrl);

        return foundUrl;
    }
}