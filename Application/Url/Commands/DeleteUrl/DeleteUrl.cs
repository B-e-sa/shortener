using Ardalis.GuardClauses;
using MediatR;
using Shortener.Infrastructure;

namespace Shortener.Application.Url.Commands.DeleteUrl
{
    public record DeleteUrlCommand(int Id) : IRequest;

    public class DeleteUrlCommandHandler(AppDbContext dbContext) : IRequestHandler<DeleteUrlCommand>
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task Handle(DeleteUrlCommand req, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Urls
                .FindAsync([req.Id], cancellationToken);

            Guard.Against.NotFound(req.Id, entity);

            _dbContext.Urls.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}