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

        public Task<Url> Get()
        {
            throw new NotImplementedException();
        }
    }
}

