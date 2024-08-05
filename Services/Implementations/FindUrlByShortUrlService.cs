using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class FindUrlByShortUrlService(IUrlRepository urlRepository) : IFindUrlByShortUrlService
    {
        private readonly  IUrlRepository _urlRepository = urlRepository;

        async public Task<Url?> Execute(string shortUrl)
        {
           return await _urlRepository.FindByShortUrl(shortUrl);
        }
    }
}