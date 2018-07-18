using Callisto.Core.Messaging.Options;
using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Callisto.Core.Messaging.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The AddCallistoMonitoring
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void AddCallistoMessaging(this IServiceCollection services, MessageOptions options, MessageExchangeConfig config)
        {
            services.AddTransient<IMessageCoordinator>(c =>
            {
                return MessageCoordinatorBuilder.Instance.Build(c);
            });
        }

        /// <summary>
        /// The UseCallistoMessaging
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/></param>
        public static void UseCallistoMessaging(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<MessageOptions>>().Value;
            var config = app.ApplicationServices.GetService<IOptions<MessageExchangeConfig>>().Value;

            var builder = MessageCoordinatorBuilder.Instance
                    .WithConnection(options)
                    .WithExchangeSetup(config);

            var consumers = app.ApplicationServices.GetServices<ConsumeBinding>();
            var publishers = app.ApplicationServices.GetServices<PublishBinding>();

            if (consumers != null)
            {
                foreach (var consumer in consumers)
                {
                    builder.ConsumeFrom(consumer.Type, consumer.QueueName);
                }
            }

            if (publishers != null)
            {
                foreach (var publisher in publishers)
                {
                    builder.PublishTo(publisher.Type, publisher.RoutingKey);
                }
            }

            Task.Run(() =>
            {
                builder.Configure();
            });
        }
    }
}
