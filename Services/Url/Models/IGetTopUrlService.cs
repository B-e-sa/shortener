namespace Shortener.Services.Url.Models
{
    public interface IGetTopUrlsService
    {
        public Task<List<Shortener.Models.Url>> Execute();
    }
}