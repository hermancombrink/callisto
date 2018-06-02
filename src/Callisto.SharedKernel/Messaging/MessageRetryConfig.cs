using System;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="MessageRetryConfig" />
    /// </summary>
    public class MessageRetryConfig
    {
        /// <summary>
        /// Gets or sets the NumberOfRetries
        /// </summary>
        public int NumberOfRetries { get; set; }

        /// <summary>
        /// Gets or sets the RetryInterval
        /// </summary>
        public TimeSpan RetryInterval { get; set; }
    }
}
