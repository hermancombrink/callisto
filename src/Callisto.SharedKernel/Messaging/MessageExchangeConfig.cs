using System.Collections.Generic;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageExchangeConfig" />
    /// </summary>
    public class MessageExchangeConfig
    {
        /// <summary>
        /// Gets or sets the ExchangeName
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        /// <remarks>
        /// direct
        /// fanout
        /// headers
        /// topic
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Consumers
        /// </summary>
        public IEnumerable<MessageConsumeConfig> Consumers { get; set; }

        /// <summary>
        /// Gets or sets the Publishers
        /// </summary>
        public IEnumerable<MessagePublishConfig> Publishers { get; set; }

        /// <summary>
        /// Gets or sets the Exchanges
        /// </summary>
        public IEnumerable<MessageExchangeConfig> Exchanges { get; set; }
    }
}
