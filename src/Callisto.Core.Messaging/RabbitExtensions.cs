using Callisto.SharedKernel.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Callisto.Core.Messaging
{
    /// <summary>
    /// Defines the <see cref="RabbitMQExtensions" />
    /// </summary>
    public static class RabbitMQExtensions
    {
        /// <summary>
        /// Defines the InfiniteRetries
        /// </summary>
        public const int InfiniteRetries = -1;

        /// <summary>
        /// The StartConsumers
        /// </summary>
        /// <param name="connection">The <see cref="IConnection"/></param>
        /// <param name="config">The <see cref="RabbitMQExtensionsConfiguration"/></param>
        /// <param name="configureConsumer">The <see cref="Action{AsyncEventingBasicConsumer}"/></param>
        /// <returns>The <see cref="IEnumerable{AsyncEventingBasicConsumer}"/></returns>
        public static IEnumerable<AsyncEventingBasicConsumer> StartConsumers(
           this IConnection connection,
           MessageConsumeConfig config,
           Action<AsyncEventingBasicConsumer> configureConsumer)
        {
            List<AsyncEventingBasicConsumer> consumers = new List<AsyncEventingBasicConsumer>();

            for (int i = 1; i <= config.WorkerCount; i++)
            {
                IModel channel = connection.CreateModel();

                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
                consumers.Add(consumer);

                consumer.ConsumerTag = $"{config.ChannelNamePrefix}#{i}";

                // This is the hook for the client to wire up their Consumer_Received event handlers and whatever else.
                configureConsumer(consumer);

                const int MustBeZero = 0;
                const bool ConfigurePrefetchCountPerConsumer = false;

                channel.BasicQos(
                   prefetchSize: MustBeZero,
                   prefetchCount: config.PrefetchCountPerWorker,
                   global: ConfigurePrefetchCountPerConsumer);

                channel.BasicConsume(
                   queue: config.QueueNameToConsume,
                   autoAck: false,
                   consumerTag: consumer.ConsumerTag,
                   consumer: consumer);
            }

            return consumers;
        }

        /// <summary>
        /// The StopConsumers
        /// </summary>
        /// <param name="consumers">The <see cref="IEnumerable{AsyncEventingBasicConsumer}"/></param>
        public static void StopConsumers(this IEnumerable<AsyncEventingBasicConsumer> consumers)
        {
            List<Exception> exceptions = null;
            foreach (AsyncEventingBasicConsumer consumer in consumers)
            {
                try
                {
                    if (consumer.Model.IsOpen)
                    {
                        consumer.Model.BasicCancel(consumer.ConsumerTag);
                        consumer.Model.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Don't create list object if we don't need it.
                    if (exceptions == null)
                    {
                        exceptions = new List<Exception>();
                    }

                    exceptions.Add(ex);
                }
            }

            if (exceptions != null)
            {
                throw new AggregateException(
                   $"One or more exceptions occurred while during {nameof(StopConsumers)}. Please see InnerExceptions for more details",
                   exceptions);
            }
        }

        /// <summary>
        /// The SetupRabbitMQTopology
        /// </summary>
        /// <param name="connection">The <see cref="IConnection"/></param>
        /// <param name="config">The <see cref="RabbitMQExtensionsConfiguration"/></param>
        public static void SetupRabbitMQTopology(this IConnection connection, MessageExchangeConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            try
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(config.ExchangeName,
                        type: config.Type,
                        durable: true,
                        autoDelete: false);

                    if (config.Consumers != null)
                    {
                        foreach (var consumer in config.Consumers)
                        {
                            channel.QueueDeclare(consumer.QueueNameToConsume,
                                durable: true,
                                exclusive: false,
                                autoDelete: false);

                            if (consumer.FailureConfig != null)
                            {
                                channel.SetupRetryQueueTopology(consumer.FailureConfig);
                            }
                        }
                    }

                    if (config.Publishers != null)
                    {
                        foreach (var publisher in config.Publishers.Where(c => !string.IsNullOrEmpty(c.QueueNameToPublish)).ToList())
                        {
                            channel.QueueDeclare(publisher.QueueNameToPublish,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false);

                            channel.QueueBind(publisher.QueueNameToPublish,
                                config.ExchangeName,
                                publisher.RoutingKey);
                        }
                    }
                }

                if (config.Exchanges != null)
                {
                    foreach (var exchange in config.Exchanges)
                    {
                        SetupRabbitMQTopology(connection, exchange);

                        using (IModel channel = connection.CreateModel())
                        {
                            channel.ExchangeBind(exchange.ExchangeName,
                               config.ExchangeName,
                               exchange.RoutingKey);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new SystemException($"Failed to setup rabbit infrastructure", ex);
            }
        }

        /// <summary>
        /// The SetupRetryQueueTopology
        /// </summary>
        /// <param name="channel">The <see cref="IModel"/></param>
        /// <param name="retryConfig">The <see cref="RabbitMQRetryConfiguration"/></param>
        public static void SetupRetryQueueTopology(this IModel channel, MessageFailureConfig retryConfig)
        {
            const string NumberOfAttemptsHeaderKey = "NumberOfAttempts";

            if (!string.IsNullOrEmpty(retryConfig.LostLetterExchangeName) && !string.IsNullOrEmpty(retryConfig.LostLetterQueueName))
            {
                channel.ExchangeDeclare(
                   exchange: retryConfig.LostLetterExchangeName,
                   type: ExchangeType.Fanout,
                   durable: true,
                   autoDelete: false);

                channel.QueueDeclare(
                   queue: retryConfig.LostLetterQueueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false);

                channel.QueueBind(
                   queue: retryConfig.LostLetterQueueName,
                   exchange: retryConfig.LostLetterExchangeName,
                   routingKey: "");
            }

            if (!string.IsNullOrEmpty(retryConfig.DeadLetterExchangeName) && !string.IsNullOrEmpty(retryConfig.DeadLetterQueueName))
            {
                channel.ExchangeDeclare(
                exchange: retryConfig.DeadLetterExchangeName,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: new Dictionary<string, object>
                {
               { "alternate-exchange", retryConfig.LostLetterExchangeName }
                });

                channel.QueueDeclare(
                   queue: retryConfig.DeadLetterQueueName,
                   durable: true,
                   exclusive: false,
                   autoDelete: false);

                channel.QueueBind(
                   queue: retryConfig.DeadLetterQueueName,
                   exchange: retryConfig.DeadLetterExchangeName,
                   routingKey: "");
            }

            if (!string.IsNullOrEmpty(retryConfig.RetryExchangeName) && !string.IsNullOrEmpty(retryConfig.RetryQueuePrefix) && retryConfig.RetryLevels != null)
            {
                channel.ExchangeDeclare(
                exchange: retryConfig.RetryExchangeName,
                type: ExchangeType.Headers,
                durable: true,
                autoDelete: false,
                arguments: new Dictionary<string, object>
                {
               { "alternate-exchange", retryConfig.DeadLetterExchangeName }
                });

                int retryLevelCount = 1;
                int attemptStartRange = 1;

                foreach (MessageRetryConfig retryLevel in retryConfig.RetryLevels)
                {
                    string retryLevelQueueName = $"{retryConfig.RetryQueuePrefix}#{retryLevelCount}";

                    channel.QueueDeclare(
                       queue: retryLevelQueueName,
                       durable: true,
                       exclusive: false,
                       autoDelete: false,
                       arguments: new Dictionary<string, object>
                       {
                  { "x-message-ttl", (int)retryLevel.RetryInterval.TotalMilliseconds },
                  { "x-dead-letter-exchange", "" }
                       });

                    if (retryLevel.NumberOfRetries == InfiniteRetries)
                    {
                        channel.QueueBind(
                           queue: retryLevelQueueName,
                           exchange: retryConfig.RetryExchangeName,
                           routingKey: "",
                              arguments: new Dictionary<string, object>
                              {
                        { "RetryToInfinity", true },
                        { "x-match", "any" }
                              });
                    }
                    else
                    {
                        for (int i = attemptStartRange; i < attemptStartRange + retryLevel.NumberOfRetries; i++)
                        {
                            channel.QueueBind(
                               queue: retryLevelQueueName,
                               exchange: retryConfig.RetryExchangeName,
                               routingKey: "",
                               arguments: new Dictionary<string, object>
                               {
                        { NumberOfAttemptsHeaderKey, i },
                        { "x-match", "any" }
                               });
                        }
                    }

                    attemptStartRange += retryLevel.NumberOfRetries;

                    retryLevelCount++;
                }
            }
        }

        /// <summary>
        /// The SendToOtherExchange
        /// </summary>
        /// <param name="channel">The <see cref="IModel"/></param>
        /// <param name="eventArgs">The <see cref="BasicDeliverEventArgs"/></param>
        /// <param name="exchangeName">The <see cref="string"/></param>
        /// <param name="routingKey">The <see cref="string"/></param>
        public static void SendToOtherExchange(this IModel channel, BasicDeliverEventArgs eventArgs, string exchangeName, string routingKey)
        {
            channel.BasicPublish(
               exchange: exchangeName,
               routingKey: routingKey,
               basicProperties: eventArgs.BasicProperties,
               body: eventArgs.Body);

            channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        /// <summary>
        /// The SendToDeadLetter
        /// </summary>
        /// <param name="channel">The <see cref="IModel"/></param>
        /// <param name="eventArgs">The <see cref="BasicDeliverEventArgs"/></param>
        /// <param name="config">The <see cref="RabbitMQExtensionsConfiguration"/></param>
        public static void SendToDeadLetter(this IModel channel, BasicDeliverEventArgs eventArgs, MessageConsumeConfig config)
        {
            channel.SendToOtherExchange(eventArgs, config.FailureConfig.DeadLetterExchangeName, eventArgs.RoutingKey);
        }

        /// <summary>
        /// The SendForRetry
        /// </summary>
        /// <param name="channel">The <see cref="IModel"/></param>
        /// <param name="eventArgs">The <see cref="BasicDeliverEventArgs"/></param>
        /// <param name="consumerconfig">The <see cref="RabbitMQExtensionsConfiguration"/></param>
        public static void SendForRetry(this IModel channel, BasicDeliverEventArgs eventArgs, MessageConsumeConfig consumerconfig)
        {
            if (consumerconfig.FailureConfig is null)
            {
                return;
            }

            const string NumberOfAttemptsHeaderKey = "NumberOfAttempts";
            if (eventArgs.BasicProperties.Headers == null)
            {
                eventArgs.BasicProperties.Headers = new Dictionary<string, object>();
            }
            IDictionary<string, object> headers = eventArgs.BasicProperties.Headers;
            if (headers.ContainsKey(NumberOfAttemptsHeaderKey))
            {
                if (int.TryParse(headers[NumberOfAttemptsHeaderKey].ToString(), out int numericNumberOfAttempts))
                {
                    numericNumberOfAttempts++;
                    headers[NumberOfAttemptsHeaderKey] = numericNumberOfAttempts;
                    if (consumerconfig.FailureConfig.RetryLevels.Last().NumberOfRetries == InfiniteRetries)
                    {
                        int totalNumberOfConfiguredRetries =
                           consumerconfig.FailureConfig.RetryLevels
                                      .Where(level => level.NumberOfRetries != InfiniteRetries)
                                      .Sum(level => level.NumberOfRetries);
                        if (numericNumberOfAttempts > totalNumberOfConfiguredRetries)
                        {
                            headers["RetryToInfinity"] = true;
                        }
                    }
                }
                else
                {
                    headers[NumberOfAttemptsHeaderKey] = 1;
                }
            }
            else
            {
                headers[NumberOfAttemptsHeaderKey] = 1;
            }

            channel.SendToOtherExchange(eventArgs, consumerconfig.FailureConfig.RetryExchangeName, consumerconfig.QueueNameToConsume);
        }

        /// <summary>
        /// Defines the RetryCount
        /// </summary>
        private static uint RetryCount = 0;

        /// <summary>
        /// The CreateResillientConnection
        /// </summary>
        /// <param name="factory">The <see cref="IConnectionFactory"/></param>
        /// <returns>The <see cref="IConnection"/></returns>
        public static IConnection CreateResillientConnection(this IConnectionFactory factory)
        {
            try
            {
                return factory.CreateConnection();
            }
            catch
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                RetryCount++;

                if (RetryCount < 10)
                {
                    return CreateResillientConnection(factory);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
