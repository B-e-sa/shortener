using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class VisitUrlService(IUrlRepository urlRepository) : IVisitUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task Execute(Url url)
        {
            await _urlRepository.Visit(url);
        }
    }
}