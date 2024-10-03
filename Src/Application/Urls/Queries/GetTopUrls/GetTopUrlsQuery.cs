using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Application.Common.Interfaces;

namespace Shortener.Application.Urls.Queries.GetTopUrls;

public record GetTopUrlsQuery() : IRequest<List<Url>>;

public class GetTopUrlsQueryHandler(IAppDbContext context) 
    : IRequestHandler<GetTopUrlsQuery, List<Url>>
{
    private readonly IAppDbContext _context = context;

    public async Task<List<Url>> Handle(GetTopUrlsQuery request, CancellationToken cancellationToken)
    {
        var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
        return await _context.Urls
            .Where(u => u.CreatedAt >= oneWeekAgo && u.Visits > 0)
            .OrderByDescending(u => u.Visits)
            .Take(10)
            .ToListAsync(cancellationToken);
    }
}