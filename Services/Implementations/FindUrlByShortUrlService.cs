using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class FindByShortUrlService(IUrlRepository urlRepository) : IFindByShortUrlService
    {
        private readonly  IUrlRepository _urlRepository = urlRepository;

        async public Task<Url?> Execute(string url)
        {
           return await _urlRepository.Find(url);
        }
    }
}