using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Messaging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Callisto.Core.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageCoordinator" />
    /// </summary>
    public class MessageCoordinator : IMessageCoordinator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCoordinator"/> class.
        /// </summary>
        /// <param name="config">The <see cref="MessageExchangeConfig"/></param>
        /// <param name="connection">The <see cref="IConnection"/></param>
        public MessageCoordinator(MessageExchangeConfig config,
            IConnection connection,
            Dictionary<Type, MessageConsumeConfig> consumers,
            Dictionary<Type, MessagePublishConfig> publishers)
        {
            Config = config;
            Connection = connection;
            Consumers = consumers;
            Publishers = publishers;

            foreach (var type in publishers.Keys)
            {
                PublishingModels.Add(type, Connection.CreateModel());
            }
        }

        /// <summary>
        /// Gets the Config
        /// </summary>
        public MessageExchangeConfig Config { get; }

        /// <summary>
        /// Gets the Connection
        /// </summary>
        public IConnection Connection { get; }

        /// <summary>
        /// Gets the Consumers
        /// </summary>
        public Dictionary<Type, MessageConsumeConfig> Consumers { get; }

        /// <summary>
        /// Gets the Publishers
        /// </summary>
        public Dictionary<Type, MessagePublishConfig> Publishers { get; }

        /// <summary>
        /// Gets the DefaultEncoding
        /// </summary>
        public Encoding DefaultEncoding { get; } = Encoding.Default;

        /// <summary>
        /// Gets the RunningConsumers
        /// </summary>
        private Dictionary<Type, IEnumerable<AsyncEventingBasicConsumer>> RunningConsumers { get; } = new Dictionary<Type, IEnumerable<AsyncEventingBasicConsumer>>();

        /// <summary>
        /// Gets the PublishingModels
        /// </summary>
        private Dictionary<Type, IModel> PublishingModels { get; } = new Dictionary<Type, IModel>();

        /// <summary>
        /// The Consume
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageDelegate">The <see cref="Func{T, Task{IMessageResult}}"/></param>
        public void Consume<T>(Func<T, Task<IMessageResult>> messageDelegate)
        {
            MessageConsumeConfig config = Consumers.Single(x => x.Key == typeof(T)).Value;
            RunningConsumers.Add(typeof(T), Connection.StartConsumers(config, consumerConfig => consumerConfig.Received += (obj, args) => Consumer_Received(obj, args, messageDelegate)));
        }

        /// <summary>
        /// The Publish
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The <see cref="T"/></param>
        public void Publish<T>(T message)
        {
            string routingKey = Publishers.Single(x => x.Key == typeof(T)).Value.RoutingKey;
            IModel model = PublishingModels.Single(x => x.Key == typeof(T)).Value;

            model.BasicPublish(Config.ExchangeName, routingKey, body: DefaultEncoding.GetBytes(JsonConvert.SerializeObject(message)));
        }

        /// <summary>
        /// The StopConsuming
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void StopConsuming<T>()
        {
            RunningConsumers.Where(x => x.Key == typeof(T))
                      .SelectMany(x => x.Value)
                      .StopConsumers();

            RunningConsumers.Remove(typeof(T));
        }

        /// <summary>
        /// The Consumer_Received
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The <see cref="object"/></param>
        /// <param name="args">The <see cref="BasicDeliverEventArgs"/></param>
        /// <param name="handler">The <see cref="Func{T, Task{IMessageResult}}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        private async Task Consumer_Received<T>(object obj, BasicDeliverEventArgs args, Func<T, Task<IMessageResult>> handler)
        {
            AsyncEventingBasicConsumer consumer = (AsyncEventingBasicConsumer)obj;
            MessageConsumeConfig config = Consumers.Single(x => x.Key == typeof(T)).Value;

            try
            {
                T contents = JsonConvert.DeserializeObject<T>(DefaultEncoding.GetString(args.Body));
                IMessageResult result = await handler(contents);

                switch (result)
                {
                    case RetryResult retry:
                        {
                            consumer.Model.SendForRetry(args, config);
                            return;
                        }

                    case DeadLetterResult dead:
                        {
                            consumer.Model.SendToDeadLetter(args, config);
                            return;
                        }

                    case SuccessResult success:
                    default:
                        {
                            consumer.Model.BasicAck(args.DeliveryTag, false);
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                consumer.Model.SendToDeadLetter(args, config);
            }
        }

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
            Connection.Abort();
        }
    }
}
