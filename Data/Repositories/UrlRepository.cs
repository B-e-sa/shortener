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

        public Task<Url> Get()
        {
            throw new NotImplementedException();
        }
    }
}

