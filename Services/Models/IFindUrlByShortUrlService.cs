using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IFindUrlByShortUrlService
    {
        public Task<Url?> Execute(string url);
    }
}