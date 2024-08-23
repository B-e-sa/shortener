using Shortener.Models;

namespace Shortener.Data.Repositories
{
    public interface IUrlRepository
    {
        Task Create(Url url);
        Task Delete(Url url);
        Task Visit(Url url);
        Task<List<Url>> GetTop();
        Task<Url?> FindByShortUrl(string url);
        Task<Url?> FindById(int id);
    }
}

