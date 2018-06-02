using System.Collections.Generic;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageFailureConfig" />
    /// </summary>
    public class MessageFailureConfig
    {
        /// <summary>
        /// Gets or sets the DeadLetterExchangeName
        /// </summary>
        public string DeadLetterExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the DeadLetterQueueName
        /// </summary>
        public string DeadLetterQueueName { get; set; }

        /// <summary>
        /// Gets or sets the LostLetterExchangeName
        /// </summary>
        public string LostLetterExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the LostLetterQueueName
        /// </summary>
        public string LostLetterQueueName { get; set; }

        /// <summary>
        /// Gets or sets the RetryExchangeName
        /// </summary>
        public string RetryExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the RetryQueuePrefix
        /// </summary>
        public string RetryQueuePrefix { get; set; }

        /// <summary>
        /// Gets or sets the RetryLevels
        /// </summary>
        public IReadOnlyCollection<MessageRetryConfig> RetryLevels { get; set; }
    }
}
