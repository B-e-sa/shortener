using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    class CreateUrlService(IUrlRepository urlRepository) : ICreateUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

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

        public async Task<Url> Execute(string url, string title)
        {
            var newUrl = new Url
            {
                Title = title,
                OriginalUrl = url,
                ShortUrl = GenerateShortUrl(),
            };

            await _urlRepository.Create(newUrl);

            return newUrl;
        }
    }
}