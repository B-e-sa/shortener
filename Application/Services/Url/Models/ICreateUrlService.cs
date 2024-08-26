namespace Shortener.Application.Services.Url.Models
{
    public interface ICreateUrlService
    {
        public Task<Domain.Entities.Url> Execute(string url, string title);
    }
}