using Callisto.Module.Notification.Options;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using System;
using System.Threading.Tasks;

namespace Callisto.Module.Notification.Email
{
    /// <summary>
    /// Defines the <see cref="SimpleSendGridEmailSender" />
    /// </summary>
    public class SimpleSendGridEmailSender : IEmailSender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSendGridEmailSender"/> class.
        /// </summary>
        /// <param name="mailOptions">The <see cref="IOptions{MailOptions}"/></param>
        /// <param name="mailFactory">The <see cref="ISendGridMalFactory"/></param>
        public SimpleSendGridEmailSender(IOptions<MailOptions> mailOptions, ISendGridMalFactory mailFactory)
        {
            Options = mailOptions.Value;
            if (string.IsNullOrWhiteSpace(Options.ApiKey))
            {
                throw new ArgumentException($"No Api key specified. Check configuration");
            }

            SendGridClient = new SendGridClient(Options.ApiKey);
            MailFactory = mailFactory;
        }

        /// <summary>
        /// Gets the Options
        /// </summary>
        private MailOptions Options { get; }

        /// <summary>
        /// Gets the SendGridClient
        /// </summary>
        private SendGridClient SendGridClient { get; }

        /// <summary>
        /// Gets the MailFactory
        /// </summary>
        public ISendGridMalFactory MailFactory { get; }

        /// <summary>
        /// The SendEmailAsync
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task SendEmailAsync(NotificationRequestModel model, NotificationType type = NotificationType.None)
        {
            var message = MailFactory.CreateMessage(type, model);
            var response = await SendGridClient.SendEmailAsync(message);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new InvalidOperationException(await response.Body.ReadAsStringAsync());
            }
        }
    }
}
