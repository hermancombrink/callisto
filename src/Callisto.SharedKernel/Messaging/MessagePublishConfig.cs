namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageConsumeConfig" />
    /// </summary>
    public class MessagePublishConfig
    {
        /// <summary>
        /// Gets or sets the RoutingKey
        /// </summary>
        public string RoutingKey { get; set; }

        /// <summary>
        /// Gets or sets the QueueName
        /// </summary>
        public string QueueNameToPublish { get; set; }
    }
}
