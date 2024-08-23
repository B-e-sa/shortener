namespace Shortener.Services.Url.Models
{
    public interface ICreateUrlService
    {
        public Task<Shortener.Models.Url> Execute(string url, string title);
    }
}