using App.Metrics;
using Callisto.Core.Metrics;
using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Messaging;
using Callisto.SharedModels.Notification.Models;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Callisto.Worker.Service.Services
{
    /// <summary>
    /// Defines the <see cref="NotificationConsumer" />
    /// </summary>
    public class NotificationConsumer : IHostedService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationConsumer"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        /// <param name="messageCoordinator">The <see cref="IMessageCoordinator"/></param>
        public NotificationConsumer(
           ICallistoSession session,
            IMessageCoordinator messageCoordinator,
            IMetrics metrics,
            IViewRenderService viewService,
            ILogger<NotificationConsumer> logger)
        {
            Session = session;
            MessageCoordinator = messageCoordinator;
            Metrics = metrics;
            ViewService = viewService;
            Logger = logger;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// Gets the MessageCoordinator
        /// </summary>
        public IMessageCoordinator MessageCoordinator { get; }

        /// <summary>
        /// Gets or sets the Metrics
        /// </summary>
        public IMetrics Metrics { get; set; }

        /// <summary>
        /// Gets the ViewService
        /// </summary>
        public IViewRenderService ViewService { get; }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        public ILogger<NotificationConsumer> Logger { get; }

        /// <summary>
        /// The StartAsync
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                MessageCoordinator.Consume<NotificationMessage>(async (msgContext) =>
                {
                    Metrics.Measure.Counter.Increment(MetricsRegistry.NotificiationCounter);

                    var template = $"email/{msgContext.Body.Type}";
                    var content = await ViewService.RenderToStringAsync(template, msgContext.Body.Request.Tokens);

                    Logger.LogInformation($"Sending mail of type {msgContext.Body.Type}");

                    msgContext.Body.Request.DefaultContent = content;

                    await Session.Notification.SubmitEmailNotification(msgContext.Body.Request, msgContext.Body.Type);

                    Logger.LogInformation($"Completed mail of type {msgContext.Body.Type}");

                    return new SuccessResult();
                });
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred : {ex.Message}");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// The StopAsync
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            MessageCoordinator.StopConsuming<NotificationMessage>();
            return Task.CompletedTask;
        }
    }
}
