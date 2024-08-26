using Shortener.Application.Services.Url.Models;
using Shortener.Infrastructure;

namespace Shortener.Application.Services.Url
{
    public class FindUrlByIdService(AppDbContext dbContext) : IFindUrlByIdService
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Domain.Entities.Url?> Execute(int id)
        {
            return await _dbContext.Urls.FindAsync(id);
        }
    }
}