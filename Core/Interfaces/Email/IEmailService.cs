using System.Threading.Tasks;

namespace Core.Interfaces.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toAddress, string subject, string message);
    }
}
