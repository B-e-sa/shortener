using Shortener.Data.Repositories;
using Shortener.Models;

namespace Shortener.Services
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