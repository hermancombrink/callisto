using Callisto.SharedModels.Notification.Enum;
using System.Collections.Generic;

namespace Callisto.Module.Notification.Options
{
    /// <summary>
    /// Defines the <see cref="TemplateItem" />
    /// </summary>
    public class TemplateItem
    {
        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }
    }
}
