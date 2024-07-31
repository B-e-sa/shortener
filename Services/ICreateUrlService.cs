using Shortener.Models;

namespace Shortener.Services
{
    public interface ICreateUrlService
    {
        public Task<Url> Execute(string url, string title);
    }
}