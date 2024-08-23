namespace Shortener.Services.Url.Models
{
    public interface IDeleteUrlService
    {
        public Task Execute(Shortener.Models.Url url);
    }
}