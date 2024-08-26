using MediatR;
using Shortener.Infrastructure;

namespace Shortener.Application.Commands.Url
{
    public record FindUrlByIdCommand(int Id) : IRequest;

    public class FindUrlByIdCommandHandler(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Domain.Entities.Url?> Execute(int id)
        {
            return await _dbContext.Urls.FindAsync(id);
        }
    }
}