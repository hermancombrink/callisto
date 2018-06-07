namespace Callisto.Worker.Service
{
    /// <summary>
    /// Defines the <see cref="TemplateOptions" />
    /// </summary>
    public class TemplateOptions
    {
        /// <summary>
        /// Gets or sets the PortalUrl
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the PortalLoginUrl
        /// </summary>
        public string LoginUrl { get; set; }

        /// <summary>
        /// Gets or sets the ResetUrl
        /// </summary>
        public string ResetUrl { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmUrl
        /// </summary>
        public string ConfirmUrl { get; set; }

        /// <summary>
        /// Gets or sets the PortalImage
        /// </summary>
        public string LogoImage { get; set; }

        /// <summary>
        /// Gets the Login
        /// </summary>
        public string Login => $"{BaseUrl}{LoginUrl}";

        /// <summary>
        /// Gets the Reset
        /// </summary>
        public string Reset => $"{BaseUrl}{ResetUrl}";

        /// <summary>
        /// Gets the Confirm
        /// </summary>
        public string Confirm => $"{BaseUrl}{ConfirmUrl}";

        /// <summary>
        /// Gets the Logo
        /// </summary>
        public string Logo => $"{BaseUrl}{LogoImage}";
    }
}
