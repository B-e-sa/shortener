using Shortener.Data.Repositories;
using Shortener.Services.Url.Models;

namespace Shortener.Services.Url
{
    public class FindUrlByIdService(IUrlRepository urlRepository) : IFindUrlByIdService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task<Shortener.Models.Url?> Execute(int id)
        {
            return await _urlRepository.FindById(id);
        }
    }
}