using MediatR;
using Shortener.Src.Infrastructure;

namespace Shortener.Src.Application.Url.Commands.CreateUrl
{
    public record CreateUrlCommand : IRequest<Domain.Entities.Url>
    {
        public string Url { get; init; }
        public string Title { get; init; }
    }

    class CreateUrlCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateUrlCommand, Domain.Entities.Url>
    {
        private readonly AppDbContext _dbContext = dbContext;

        private static string GenerateShortUrl()
        {
            string shortUrl = "";
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new();
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(0, chars.Length);
                shortUrl += chars[index];
            }
            return shortUrl;
        }

        public async Task<Domain.Entities.Url> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            var newUrl = new Domain.Entities.Url
            {
                Title = request.Title,
                OriginalUrl = request.Url,
                ShortUrl = GenerateShortUrl(),
            };

            _dbContext.Urls.Add(newUrl);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newUrl;
        }
    }
}