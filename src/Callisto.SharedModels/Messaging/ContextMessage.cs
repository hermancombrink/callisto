using Callisto.SharedModels.Session;

namespace Callisto.SharedModels.Messaging
{
    /// <summary>
    /// Defines the <see cref="ContextMessage{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PublishContextMessage<T>
    {
        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public long CurrentCompanyRef { get; set; }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public T Body { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="ConsumeContextMessage{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsumeContextMessage<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumeContextMessage{T}"/> class.
        /// </summary>
        public ConsumeContextMessage(ICallistoSession session, T body)
        {
            Session = session;
            Body = body;
        }

        /// <summary>
        /// Gets or sets the Session
        /// </summary>
        public ICallistoSession Session { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public T Body { get; set; }
    }
}
