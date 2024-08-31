using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Infrastructure;

namespace Shortener.Application.Url.Queries.GetTopUrls
{
    public record GetTopUrlsQuery() : IRequest<List<Domain.Entities.Url>>;

    public class GetTopUrlsQueryHandler(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<Domain.Entities.Url>> Execute()
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
            return await _dbContext.Urls
                .Where(u => u.CreatedAt >= oneWeekAgo)
                .OrderByDescending(u => u.Visits)
                .Take(10)
                .ToListAsync();
        }
    }
}