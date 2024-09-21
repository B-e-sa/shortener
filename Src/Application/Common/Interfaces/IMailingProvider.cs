namespace Shortener.Application.Common.Interfaces
{
    public interface IMailingProvider
    {
        Task SendVerificationCode(string username, string userEmail, string Code);
    }
}
