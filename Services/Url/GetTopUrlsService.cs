using Shortener.Data.Repositories;
using Shortener.Services.Url.Models;

namespace Shortener.Services.Url
{
    public class GetTopUrlsService(IUrlRepository urlRepository) : IGetTopUrlsService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task<List<Shortener.Models.Url>> Execute()
        {
            return await _urlRepository.GetTop();
        }
    }
}