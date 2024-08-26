using Shortener.Application.Services.Url.Models;
using Shortener.Infrastructure;

namespace Shortener.Application.Services.Url
{
    public class DeleteUrlService(AppDbContext dbContext) : IDeleteUrlService
    {
        private readonly AppDbContext _dbContext = dbContext; 

        public async Task Execute(Domain.Entities.Url url)
        {
            _dbContext.Urls.Remove(url);
            await _dbContext.SaveChangesAsync();
        }
    }
}