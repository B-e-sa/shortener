using Shortener.Models;

namespace Shortener.Data.Repositories
{
    public interface IUrlRepository
    {
        Task<Url> Create(Url url);
        Task<Url> Get();
        Task<Url?> Find(string url);
        Task Visit(Url url);
    }
}

