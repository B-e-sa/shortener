namespace Shortener.Application.Common.Interfaces;

public interface IMailingProvider
{
    Task SendVerificationCode(string username, string userEmail, string code);
    Task SendNewPasswordVerificationCode(string username, string userEmail, string code);
}