namespace Shortener.Application.Services.Url.Models
{
    public interface IVisitUrlService
    {
        public Task Execute(Domain.Entities.Url url);
    }
}