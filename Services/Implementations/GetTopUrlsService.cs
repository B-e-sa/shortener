using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class GetTopUrlsService(IUrlRepository urlRepository) : IGetTopUrlsService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task<List<Url>> Execute()
        {
            return await _urlRepository.GetTop();
        }
    }
}