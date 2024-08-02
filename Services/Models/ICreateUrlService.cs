using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface ICreateUrlService
    {
        public Task<Url> Execute(string url, string title);
    }
}