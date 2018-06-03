using System;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="ConsumeBinding" />
    /// </summary>
    public class ConsumeBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeBinding"/> class.
        /// </summary>
        public ConsumeBinding()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeBinding"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/></param>
        /// <param name="queueName">The <see cref="string"/></param>
        public ConsumeBinding(Type type, string queueName)
        {
            Type = type;
            QueueName = queueName;
        }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the QueueName
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// The SetBinding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routingKey">The <see cref="string"/></param>
        /// <returns>The <see cref="ConsumeBinding"/></returns>
        public static ConsumeBinding SetBinding<T>(string queueName)
        {
            return new ConsumeBinding(typeof(T), queueName);
        }
    }
}
