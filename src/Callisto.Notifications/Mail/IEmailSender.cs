using System.Threading.Tasks;

namespace Callisto.Notifications.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
