using Callisto.SharedModels.Auth;

namespace Callisto.SharedModels.Session
{
    /// <summary>
    /// Defines the <see cref="ICallistoSession" />
    /// </summary>
    public interface ICallistoSession
    {
        /// <summary>
        /// Gets the Authentication
        /// </summary>
        IAuthenticationModule Authentication { get; }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        string UserName { get; }
    }
}
