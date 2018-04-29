namespace Callisto.Module.Authentication.Options
{
    /// <summary>
    /// Defines the <see cref="AuthOptions" />
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Gets or sets the RequiredLength
        /// </summary>
        public int RequiredLength { get; set; } = 6;

        /// <summary>
        /// Gets or sets a value indicating whether RequireNonAlphanumeric
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; } 

        /// <summary>
        /// Gets or sets a value indicating whether RequireLowercase
        /// </summary>
        public bool RequireLowercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RequireUppercase
        /// </summary>
        public bool RequireUppercase { get; set; } 

        /// <summary>
        /// Gets or sets a value indicating whether RequireDigit
        /// </summary>
        public bool RequireDigit { get; set; } 
    }
}
