namespace Shortener.Services.Url.Models
{
    public interface IFindUrlByShortUrlService
    {
        public Task<Shortener.Models.Url?> Execute(string url);
    }
}