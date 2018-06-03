using System;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="CallistoWorkerSession" />
    /// </summary>
    public class CallistoWorkerSession : BaseSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallistoConsumerSession"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public CallistoWorkerSession(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public override bool IsAuthenticated => true;

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public override string UserName => "System";

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public override long CurrentCompanyRef => 0;

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public override string EmailAddress => "n/a";
    }
}
