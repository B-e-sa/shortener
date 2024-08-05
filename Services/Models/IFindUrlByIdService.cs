using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IFindUrlByIdService
    {
        Task<Url?> Execute(string id);
    }
}