namespace Shortener.Application.Services.Url.Models
{
    public interface IFindUrlByIdService
    {
        Task<Domain.Entities.Url?> Execute(int id);
    }
}