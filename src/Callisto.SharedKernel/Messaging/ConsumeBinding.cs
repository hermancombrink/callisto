using System;

namespace Callisto.SharedKernel.Messaging
{
    /// <summary>
    /// Defines the <see cref="ConsumeBinding" />
    /// </summary>
    public class ConsumeBinding
    {
        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the QueueName
        /// </summary>
        public string QueueName { get; set; }
    }
}
