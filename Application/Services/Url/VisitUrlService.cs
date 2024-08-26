using Shortener.Application.Services.Url.Models;
using Shortener.Infrastructure;

namespace Shortener.Application.Services.Url
{
    public class VisitUrlService(AppDbContext dbContext) : IVisitUrlService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task Execute(Domain.Entities.Url url)
        {
            url.Visits += 1;
            _dbContext.Urls.Update(url);
            await _dbContext.SaveChangesAsync();
        }
    }
}