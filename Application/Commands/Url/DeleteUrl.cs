using MediatR;
using Shortener.Infrastructure;

namespace Shortener.Application.Commands.Url
{
    public record DeleteUrlCommand(int Id) : IRequest;

    public class DeleteUrlCommandHandler(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext; 

        public async Task Execute(Domain.Entities.Url url)
        {
            _dbContext.Urls.Remove(url);
            await _dbContext.SaveChangesAsync();
        }
    }
}