namespace Shortener.Application.Services.Url.Models
{
    public interface IFindUrlByShortUrlService
    {
        public Task<Domain.Entities.Url?> Execute(string url);
    }
}