using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;
using Shortener.Domain.Common.Exceptions.Urls;

namespace Shortener.Application.Urls.Queries.FindUrlByShortUrl;

public record FindUrlByShortUrlQuery(string ShortUrl) : IRequest<Domain.Entities.Url>;

public class FindUrlByShortUrlQueryHandler(IAppDbContext context) : IRequestHandler<FindUrlByShortUrlQuery, Domain.Entities.Url>
{
    private readonly IAppDbContext _context = context;

    public async Task<Domain.Entities.Url> Handle(FindUrlByShortUrlQuery req, CancellationToken cancellationToken)
    {
        var foundUrl = await _context.Urls
            .Where(u => u.ShortUrl == req.ShortUrl)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new UrlNotFoundException();

        foundUrl.Visits += 1;
        _context.Urls.Update(foundUrl);
        await _context.SaveChangesAsync(cancellationToken);

        return foundUrl;
    }
}