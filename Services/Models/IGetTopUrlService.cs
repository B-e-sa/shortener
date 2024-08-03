using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IGetTopUrlsService
    {
        public Task<List<Url>> Execute();
    }
}