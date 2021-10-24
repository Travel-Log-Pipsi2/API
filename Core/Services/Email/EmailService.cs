using Core.Interfaces.Email;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Core.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string message)
        {
            var mailMessage = new MailMessage(_config["SMTP:Name"], toAddress, subject, message);

            using var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}
