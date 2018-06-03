using Callisto.SharedModels.Messaging;
using System;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="CallistoConsumerSession" />
    /// </summary>
    public class CallistoConsumerSession<T> : BaseSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallistoConsumerSession"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public CallistoConsumerSession(IServiceProvider serviceProvider, PublishContextMessage<T> message) : base(serviceProvider)
        {
            IsAuthenticated = message.IsAuthenticated;
            UserName = message.UserName;
            CurrentCompanyRef = message.CurrentCompanyRef;
            EmailAddress = message.EmailAddress;
        }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public override bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public override string UserName { get; }

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public override long CurrentCompanyRef { get; }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public override string EmailAddress { get; }
    }
}
