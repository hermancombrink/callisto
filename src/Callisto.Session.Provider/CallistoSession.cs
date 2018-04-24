using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Session;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="CallistoSession" />
    /// </summary>
    public class CallistoSession : ICallistoSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallistoSession"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IAuthenticationModule"/></param>
        public CallistoSession(IAuthenticationModule authentication)
        {
            Authentication = authentication;
        }

        /// <summary>
        /// Gets or sets the Authentication
        /// </summary>
        public IAuthenticationModule Authentication { get; set; }
    }
}
