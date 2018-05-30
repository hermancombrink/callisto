using System;

namespace Callisto.Module.Assets.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="AssetModel" />
    /// </summary>
    public class AssetModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the AssetNumber
        /// </summary>
        public string AssetNumber { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }
    }
}
