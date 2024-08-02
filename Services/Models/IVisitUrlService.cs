using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IVisitUrlService
    {
        public Task Execute(Url url);
    }
}