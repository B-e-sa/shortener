using Ardalis.GuardClauses;
using MediatR;
using Shortener.Infrastructure;

namespace Shortener.Application.Url.Queries.FindUrlById
{
    public sealed record FindUrlByIdQuery(int Id) : IRequest<Domain.Entities.Url>;

    internal sealed class FindUrlByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<FindUrlByIdQuery, Domain.Entities.Url>
    {
        readonly AppDbContext _dbContext = dbContext;

        public async Task<Domain.Entities.Url> Handle(
            FindUrlByIdQuery req,
            CancellationToken cancellationToken)
        {
            var foundUrl = await _dbContext.Urls.FindAsync([req.Id], cancellationToken: cancellationToken);

            Guard.Against.NotFound(req.Id, foundUrl);

            return foundUrl;
        }
    }
}