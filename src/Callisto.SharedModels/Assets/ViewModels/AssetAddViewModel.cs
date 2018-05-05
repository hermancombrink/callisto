using System;

namespace Callisto.SharedModels.Assets.ViewModels
{
    /// <summary>
    /// Defines the <see cref="AssetAddViewModel" />
    /// </summary>
    public class AssetAddViewModel
    {
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

        /// <summary>
        /// Gets or sets the ParentId
        /// </summary>
        public Guid ParentId { get; set; }
    }
}
