using Shortener.Data.Repositories;
using Shortener.Services.Url.Models;

namespace Shortener.Services.Url
{
    public class FindUrlByShortUrlService(IUrlRepository urlRepository) : IFindUrlByShortUrlService
    {
        private readonly  IUrlRepository _urlRepository = urlRepository;

        async public Task<Shortener.Models.Url?> Execute(string shortUrl)
        {
           return await _urlRepository.FindByShortUrl(shortUrl);
        }
    }
}