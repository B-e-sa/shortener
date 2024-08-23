namespace Shortener.Services.Url.Models
{
    public interface IVisitUrlService
    {
        public Task Execute(Shortener.Models.Url url);
    }
}