namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageConsumeConfig" />
    /// </summary>
    public class MessageConsumeConfig
    {
        /// <summary>
        /// Gets or sets the WorkerCount
        /// </summary>
        public int WorkerCount { get; set; }

        /// <summary>
        /// Gets or sets the PrefetchCountPerWorker
        /// </summary>
        public ushort PrefetchCountPerWorker { get; set; }

        /// <summary>
        /// Gets or sets the QueueNameToConsume
        /// </summary>
        public string QueueNameToConsume { get; set; }

        /// <summary>
        /// Gets or sets the ChannelNamePrefix
        /// </summary>
        public string ChannelNamePrefix { get; set; }

        /// <summary>
        /// Gets or sets the FailureConfig
        /// </summary>
        public MessageFailureConfig FailureConfig { get; set; }
    }
}
