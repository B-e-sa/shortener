namespace Shortener.Application.Common.Interfaces
{
    public interface IMailingProvider
    {
        void SendVerificationCode(string username, string userEmail, string Code);
    }
}
