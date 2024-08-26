namespace Shortener.Application.Services.Url.Models
{
    public interface IGetTopUrlsService
    {
        public Task<List<Domain.Entities.Url>> Execute();
    }
}