using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Infrastructure;

namespace Shortener.Application.Url.Queries.FindUrlByShortUrl
{
    public record FindUrlByShortUrlQuery(string ShortUrl) : IRequest<Domain.Entities.Url>;

    public class FindUrlByShortUrlQueryHandler(AppDbContext dbContext) : IRequestHandler<FindUrlByShortUrlQuery, Domain.Entities.Url>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Domain.Entities.Url> Handle(FindUrlByShortUrlQuery req, CancellationToken cancellationToken)
        {
            var foundUrl = await _dbContext.Urls
                .Where(u => u.ShortUrl == req.ShortUrl)
                .FirstOrDefaultAsync(cancellationToken);

            Guard.Against.NotFound(req.ShortUrl, foundUrl);

            foundUrl.Visits += 1;
            _dbContext.Urls.Update(foundUrl);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return foundUrl;
        }
    }
}