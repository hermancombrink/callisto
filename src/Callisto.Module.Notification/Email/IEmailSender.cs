using System.Threading.Tasks;

namespace Callisto.Module.Notification.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
