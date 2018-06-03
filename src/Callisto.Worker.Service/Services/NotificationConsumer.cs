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
        public NotificationConsumer(ICallistoSession session,
            IMessageCoordinator messageCoordinator,
            IMetrics metrics,
            ILogger<NotificationConsumer> logger)
        {
            Session = session;
            MessageCoordinator = messageCoordinator;
            Metrics = metrics;
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
        /// Gets the Logger
        /// </summary>
        public ILogger<NotificationConsumer> Logger { get; }

        /// <summary>
        /// The StartAsync
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                MessageCoordinator.Consume<NotificationMessage>(async (msgContext) =>
                {
                    Metrics.Measure.Counter.Increment(MetricsRegistry.NotificiationCounter);

                    Logger.LogInformation(msgContext.Body.Request.DefaultSubject);

                    return await Task.FromResult(new SuccessResult());
                });
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred : {ex.Message}");
            }

            await Task.Delay(Timeout.Infinite, cancellationToken);
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
