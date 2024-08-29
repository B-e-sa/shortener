using MediatR;
using Shortener.Infrastructure;

namespace Shortener.Application.Url.Commands.VisitUrl
{
     public record VisitUrlCommand(string Url) : IRequest;

    public class VisitUrlCommandHandler(AppDbContext dbContext)
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