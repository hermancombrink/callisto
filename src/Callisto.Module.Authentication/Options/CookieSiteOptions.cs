namespace Callisto.Module.Authentication.Options
{
    /// <summary>
    /// Defines the <see cref="CookieSiteOptions" />
    /// </summary>
    public class CookieSiteOptions
    {
        /// <summary>
        /// Gets or sets the CookiePath
        /// </summary>
        public string CookiePath { get; set; }

        /// <summary>
        /// Gets or sets the CookieDomain
        /// </summary>
        public string CookieDomain { get; set; }

        /// <summary>
        /// Gets or sets the ExpireTimeSpanInMinutes
        /// </summary>
        public int ExpireTimeSpanInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the ReturnUrlParameter
        /// </summary>
        public string ReturnUrlParameter { get; set; }

        /// <summary>
        /// Gets or sets the AccessDeniedPath
        /// </summary>
        public string AccessDeniedPath { get; set; }

        /// <summary>
        /// Gets or sets the LogoutPath
        /// </summary>
        public string LogoutPath { get; set; }

        /// <summary>
        /// Gets or sets the LoginPath
        /// </summary>
        public string LoginPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SlidingExpiration
        /// </summary>
        public bool SlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CookieHttpOnly
        /// </summary>
        public bool CookieHttpOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CookieSecure
        /// </summary>
        public bool CookieSecure { get; set; }
    }
}
