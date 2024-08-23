using Shortener.Data.Repositories;
using Shortener.Services.Url.Models;

namespace Shortener.Services.Url
{
    public class DeleteUrlService(IUrlRepository urlRepository) : IDeleteUrlService
    {
        private readonly IUrlRepository _urlRepository = urlRepository; 

        public async Task Execute(Shortener.Models.Url url)
        {
            await _urlRepository.Delete(url);
        }
    }
}