using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IFindByShortUrlService
    {
        public Task<Url?> Execute(string url);
    }
}