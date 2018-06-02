using Callisto.Core.Messaging.Options;
using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Messaging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Callisto.Core.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageCoordinatorBuilder" />
    /// </summary>
    public class MessageCoordinatorBuilder
    {
        /// <summary>
        /// Defines the _lazyInstance
        /// </summary>
        private static readonly Lazy<MessageCoordinatorBuilder> _lazyInstance = new Lazy<MessageCoordinatorBuilder>(() => new MessageCoordinatorBuilder(), true);

        /// <summary>
        /// Prevents a default instance of the <see cref="MessageCoordinatorBuilder"/> class from being created.
        /// </summary>
        private MessageCoordinatorBuilder()
        {
        }

        /// <summary>
        /// Gets the Instance
        /// </summary>
        public static MessageCoordinatorBuilder Instance
        {
            get
            {
                return _lazyInstance.Value;
            }
        }

        /// <summary>
        /// Gets or sets the Factory
        /// </summary>
        public IConnectionFactory Factory { get; set; }

        /// <summary>
        /// Gets or sets the Config
        /// </summary>
        public MessageExchangeConfig TopologyConfig { get; set; }

        /// <summary>
        /// Gets the ConsumerConfig
        /// </summary>
        public Dictionary<Type, MessageConsumeConfig> ConsumerConfig { get; } = new Dictionary<Type, MessageConsumeConfig>();

        /// <summary>
        /// Gets the PublishConfig
        /// </summary>
        public Dictionary<Type, MessagePublishConfig> PublishConfig { get; } = new Dictionary<Type, MessagePublishConfig>();

        /// <summary>
        /// The Build
        /// </summary>
        /// <returns>The <see cref="IMessageCoordinator"/></returns>
        public IMessageCoordinator Build()
        {
            return new MessageCoordinator(TopologyConfig, Factory.CreateConnection(), ConsumerConfig, PublishConfig);
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="exchangeConfig">The <see cref="MessageExchangeConfig"/></param>
        public void Configure(MessageExchangeConfig exchangeConfig = null)
        {
            exchangeConfig = exchangeConfig ?? TopologyConfig;
            using (var connection = Factory.CreateConnection())
            {
                connection.SetupRabbitMQTopology(TopologyConfig);
            }
        }

        /// <summary>
        /// The ConsumeFrom
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName">The <see cref="string"/></param>
        /// <returns>The <see cref="MessageCoordinatorBuilder"/></returns>
        public MessageCoordinatorBuilder ConsumeFrom(Type type, string queueName, MessageExchangeConfig exchangeConfig = null)
        {
            exchangeConfig = exchangeConfig ?? TopologyConfig;
            if (exchangeConfig.Consumers != null)
            {
                var config = exchangeConfig.Consumers.FirstOrDefault(c => c.QueueNameToConsume == queueName);
                ConsumerConfig.Add(type, config);
            }
            return this;
        }

        /// <summary>
        /// The PublishTo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routingKey">The <see cref="string"/></param>
        /// <returns>The <see cref="IMessageCoordinatorBuilder"/></returns>
        public MessageCoordinatorBuilder PublishTo(Type type, string routingKey, MessageExchangeConfig exchangeConfig = null)
        {
            exchangeConfig = exchangeConfig ?? TopologyConfig;
            if (exchangeConfig.Publishers != null)
            {
                var config = exchangeConfig.Publishers.FirstOrDefault(c => c.RoutingKey == routingKey);
                PublishConfig.Add(type, config);
            }
            return this;
        }

        /// <summary>
        /// The WithConnection
        /// </summary>
        /// <param name="options">The <see cref="MessageOptions"/></param>
        /// <returns>The <see cref="MessageCoordinatorBuilder"/></returns>
        public MessageCoordinatorBuilder WithConnection(MessageOptions options)
        {
            Factory = new ConnectionFactory()
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,
                DispatchConsumersAsync = true,
                Ssl = new SslOption()
                {
                    Enabled = options.Ssl,
                    ServerName = options.SslServer,
                    Version = System.Security.Authentication.SslProtocols.Tls12,
                }
            };
            return this;
        }

        /// <summary>
        /// The WithExchangeSetup
        /// </summary>
        /// <param name="options">The <see cref="MessageExchangeConfig"/></param>
        /// <returns>The <see cref="MessageCoordinatorBuilder"/></returns>
        public MessageCoordinatorBuilder WithExchangeSetup(MessageExchangeConfig options)
        {
            TopologyConfig = options;
            return this;
        }
    }
}
