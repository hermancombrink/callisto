using System.Collections.Generic;

namespace Callisto.Module.Notification.Options
{
    /// <summary>
    /// Defines the <see cref="MailOptions" />
    /// </summary>
    public class MailOptions
    {
        /// <summary>
        /// Gets or sets the ApiKey
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the FromAddress
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// Gets or sets the FromDisplayName
        /// </summary>
        public string FromDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the Templates
        /// </summary>
        public List<TemplateItem> Templates { get; set; } = new List<TemplateItem>();
    }
}
