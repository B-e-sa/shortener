using Microsoft.EntityFrameworkCore;
using Shortener.Models;

namespace Shortener.Data.Repositories
{
    public class UrlRepository(AppDbContext dbContext) : IUrlRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Url> Create(Url url)
        {
            _dbContext.Urls.Add(url);
            await _dbContext.SaveChangesAsync();
            return url;
        }

        public async Task<Url?> Find(string url)
        {
            return await _dbContext.Urls
                .Where(u => u.ShortUrl == url)
                .FirstOrDefaultAsync();
        }

        public async Task Visit(Url url)
        {
            url.Visits += 1;
            _dbContext.Urls.Update(url);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Url>> GetTop()
        {
            return await _dbContext.Urls
                .OrderByDescending(u => u.Visits)
                .Take(10)
                .ToListAsync();
        }
    }
}

