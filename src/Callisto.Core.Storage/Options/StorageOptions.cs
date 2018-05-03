namespace Callisto.Core.Storage.Options
{
    /// <summary>
    /// Defines the <see cref="StorageOptions" />
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// Gets or sets the AccountKey
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the Images
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        /// Gets or sets the ScaledImages
        /// </summary>
        public string ScaledImages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseScaling
        /// </summary>
        public bool UseScaling { get; set; }
    }
}
