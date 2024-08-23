using Shortener.Data.Repositories;
using Shortener.Services.Url.Models;

namespace Shortener.Services.Url
{
    public class VisitUrlService(IUrlRepository urlRepository) : IVisitUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task Execute(Shortener.Models.Url url)
        {
            await _urlRepository.Visit(url);
        }
    }
}