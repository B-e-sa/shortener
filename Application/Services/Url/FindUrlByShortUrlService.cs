using Microsoft.EntityFrameworkCore;
using Shortener.Application.Services.Url.Models;
using Shortener.Infrastructure;

namespace Shortener.Application.Services.Url
{
    public class FindUrlByShortUrlService(AppDbContext dbContext) : IFindUrlByShortUrlService
    {
        private readonly  AppDbContext _dbContext = dbContext;

        async public Task<Domain.Entities.Url?> Execute(string shortUrl)
        {
           return await _dbContext.Urls
                .Where(u => u.ShortUrl == shortUrl)
                .FirstOrDefaultAsync();
        }
    }
}