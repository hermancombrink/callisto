using System;
using System.Threading.Tasks;
using Callisto.SharedKernal.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Callisto.Notifications.Mail
{
    public class SimpleSendGridEmailSender : IEmailSender
    {
        public SimpleSendGridEmailSender(IOptions<MailOptions> mailOptions)
        {
            Options = mailOptions.Value;
            if (string.IsNullOrWhiteSpace(Options.ApiKey))
            {
                throw new ArgumentException($"No Api key specified. Check configuration");
            }

            SendGridClient = new SendGridClient(Options.ApiKey);
        }

        private MailOptions Options { get; }

        private SendGridClient SendGridClient { get; }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await SendGridClient.SendEmailAsync(GetMessage(email, subject, message));
        }

        private SendGridMessage GetMessage(string email, string subject, string message)
        {
            try
            {
                var mailMessage = new SendGridMessage();
                mailMessage.AddTo(email);
                mailMessage.From = new EmailAddress(Options.FromAddress, Options.FromDisplayName);
                mailMessage.Subject = subject;
                mailMessage.HtmlContent = message;
                mailMessage.PlainTextContent = message;

                return mailMessage;
            }
            catch (Exception ex)
            {
                throw new FormatException($"Failed to construct mail message", ex);
            }

        }
    }
}
