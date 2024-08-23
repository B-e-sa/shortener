using Shortener.Data.Repositories;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class FindUrlByIdService(IUrlRepository urlRepository) : IFindUrlByIdService
    {
        private readonly IUrlRepository _urlRepository = urlRepository;

        public async Task<Url?> Execute(int id)
        {
            return await _urlRepository.FindById(id);
        }
    }
}