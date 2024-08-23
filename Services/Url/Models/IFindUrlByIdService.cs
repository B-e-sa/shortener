namespace Shortener.Services.Url.Models
{
    public interface IFindUrlByIdService
    {
        Task<Shortener.Models.Url?> Execute(int id);
    }
}