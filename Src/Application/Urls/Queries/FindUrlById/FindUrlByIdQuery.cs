using Ardalis.GuardClauses;
using MediatR;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Urls;

namespace Shortener.Application.Urls.Queries.FindUrlById;

public sealed record FindUrlByIdQuery(int Id) : IRequest<Domain.Entities.Url>;

internal sealed class FindUrlByIdQueryHandler(IAppDbContext context) : IRequestHandler<FindUrlByIdQuery, Domain.Entities.Url>
{
    readonly IAppDbContext _context = context;

    public async Task<Domain.Entities.Url> Handle(
        FindUrlByIdQuery req,
        CancellationToken cancellationToken)
    {
        var foundUrl = await _context.Urls.FindAsync([req.Id], cancellationToken: cancellationToken)
        ?? throw new UrlNotFoundException();

        return foundUrl;
    }
}