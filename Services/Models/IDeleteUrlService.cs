using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IDeleteUrlService
    {
        public Task Execute(Url url);
    }
}