using Shortener.Models;

namespace Shortener.Services
{
    public interface IFindByShortUrlService
    {
        public Task<Url?> Execute(string url);
    }
}