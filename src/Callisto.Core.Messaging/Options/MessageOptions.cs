namespace Callisto.Core.Messaging.Options
{
    /// <summary>
    /// Defines the <see cref="MessageOptions" />
    /// </summary>
    public class MessageOptions
    {
        /// <summary>
        /// Gets or sets the HostName
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Ssl
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// Gets or sets the SslServer
        /// </summary>
        public string SslServer { get; set; }

        /// <summary>
        /// Gets or sets the AppId
        /// </summary>
        public string AppId { get; set; }
    }
}
