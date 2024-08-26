using MediatR;
using Microsoft.EntityFrameworkCore;
using Shortener.Infrastructure;

namespace Shortener.Application.Commands.Url
{
    public record FindUrlByShortUrlCommand(string ShortUrl) : IRequest;

    public class FindUrlByShortUrlCommandHandler(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        async public Task<Domain.Entities.Url?> Execute(string shortUrl)
        {
            return await _dbContext.Urls
                 .Where(u => u.ShortUrl == shortUrl)
                 .FirstOrDefaultAsync();
        }
    }
}