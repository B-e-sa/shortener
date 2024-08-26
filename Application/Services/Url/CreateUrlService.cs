using Shortener.Application.Services.Url.Models;
using Shortener.Infrastructure;

namespace Shortener.Application.Services.Url
{
    class CreateUrlService(AppDbContext dbContext) : ICreateUrlService
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

        public async Task<Domain.Entities.Url> Execute(string url, string title)
        {
            var newUrl = new Domain.Entities.Url
            {
                Title = title,
                OriginalUrl = url,
                ShortUrl = GenerateShortUrl(),
            };

            _dbContext.Urls.Add(newUrl);
            await _dbContext.SaveChangesAsync();

            return newUrl;
        }
    }
}