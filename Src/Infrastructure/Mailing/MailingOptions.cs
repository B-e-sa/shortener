namespace Shortener.Infrastructure.Mailing
{
    public class MailingOptions
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Smtp { get; init; }
        public int Port { get; init; }
        
    }
}