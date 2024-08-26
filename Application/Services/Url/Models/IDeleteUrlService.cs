namespace Shortener.Application.Services.Url.Models
{
    public interface IDeleteUrlService
    {
        public Task Execute(Domain.Entities.Url url);
    }
}