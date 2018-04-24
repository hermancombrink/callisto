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
        IAuthenticationModule Authentication { get; set; }
    }
}
