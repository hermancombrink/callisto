using System;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="PublishBinding" />
    /// </summary>
    public class PublishBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublishBinding"/> class.
        /// </summary>
        public PublishBinding()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishBinding"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/></param>
        /// <param name="routingKey">The <see cref="string"/></param>
        public PublishBinding(Type type, string routingKey)
        {
            Type = type;
            RoutingKey = routingKey;
        }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the RoutingKey
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// The SetBinding
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routingKey">The <see cref="string"/></param>
        /// <returns>The <see cref="PublishBinding"/></returns>
        public static PublishBinding SetBinding<T>(string routingKey)
        {
            return new PublishBinding(typeof(T), routingKey);
        }
    }
}
