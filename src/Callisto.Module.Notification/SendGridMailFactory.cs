using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;

namespace Callisto.Module.Notification
{
    /// <summary>
    /// Defines the <see cref="SendGridMailFactory" />
    /// </summary>
    public class SendGridMailFactory : ISendGridMalFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridMailFactory"/> class.
        /// </summary>
        /// <param name="templateOptions">The <see cref="IOptions{TemplateOptions}"/></param>
        public SendGridMailFactory(IOptions<MailOptions> templateOptions)
        {
            Options = templateOptions.Value;
        }

        /// <summary>
        /// Gets the Options
        /// </summary>
        private MailOptions Options { get; }

        /// <summary>
        /// The CreateMessage
        /// </summary>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="SendGridMessage"/></returns>
        public SendGridMessage CreateMessage(NotificationType type, NotificationRequestModel model)
        {
            try
            {
                var mailMessage = new SendGridMessage();
                mailMessage.AddTo(model.DefaultDestination);
                mailMessage.From = new EmailAddress(Options.FromAddress, Options.FromDisplayName);
                mailMessage.Subject = model.DefaultSubject;
                mailMessage.HtmlContent = model.DefaultContent;
                mailMessage.PlainTextContent = model.DefaultContent;

                var template = Options.Templates?.FirstOrDefault(c => c.Type == type);

                if (template != null)
                {
                    mailMessage.AddSubstitutions(model.Tokens);
                    if (string.IsNullOrEmpty(template.Id))
                    {
                        mailMessage.SetTemplateId(template.Id);
                    }
                }

                return mailMessage;
            }
            catch (Exception ex)
            {
                throw new FormatException($"Failed to construct mail message", ex);
            }
        }
    }
}
