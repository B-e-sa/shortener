using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Shortener.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace Shortener.Infrastructure.Mailing
{
    public sealed class MailingProvider(IOptions<MailingOptions> options) : IMailingProvider
    {
        private readonly MailingOptions _options = options.Value;

        public async Task SendVerificationCode(string username, string userEmail, string Code)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_options.Username, _options.Email));
            email.To.Add(new MailboxAddress(username, userEmail));

            email.Subject = "Confirmation code";

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = @$"<div>
                                <div style='color: black; display: flex; flex-direction: column; align-items: center; width: 600px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;'>
                                <div style='background-color: #0062FF; text-align: center; padding: 5px; width: 100%;'>
                                    <p style='color: white; font-size: 24px;'>Shortener</p>
                                </div>
                                <p>Hello, {username}</p>
                                <div>
                                    <p>This is your confirmation code:</p>
                                    <div style='margin: auto; width: 50%; border: 2px solid black; padding: 20px; text-align: center;'>
                                    {Code}
                                    </div>
                                </div>
                                <p>If you weren't the one who made this request, just ignore this email.</p>
                                <p>Thanks, <br />Shortener team.</p>
                                </div>
                            </div>"
            };

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Smtp, _options.Port, false);
            smtp.Authenticate(_options.Username, _options.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendNewPasswordVerificationCode(string username, string userEmail, string Code)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_options.Username, _options.Email));
            email.To.Add(new MailboxAddress(username, userEmail));

            email.Subject = "Confirmation code";

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = @$"<div>
                                <div style='color: black; display: flex; flex-direction: column; align-items: center; width: 600px; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;'>
                                <div style='background-color: #0062FF; text-align: center; padding: 5px; width: 100%;'>
                                    <p style='color: white; font-size: 24px;'>Shortener</p>
                                </div>
                                <p>Hello, {username}</p>
                                <div>
                                    <p>This is your confirmation new password confirmation code:</p>
                                    <div style='margin: auto; width: 50%; border: 2px solid black; padding: 20px; text-align: center;'>
                                    {Code}
                                    </div>
                                </div>
                                <p>If you weren't the one who made this request, just ignore this email.</p>
                                <p>Thanks, <br />Shortener team.</p>
                                </div>
                            </div>"
            };

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Smtp, _options.Port, false);
            smtp.Authenticate(_options.Username, _options.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
