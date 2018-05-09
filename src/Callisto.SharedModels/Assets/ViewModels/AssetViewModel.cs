using System;

namespace Callisto.SharedModels.Assets.ViewModels
{
    /// <summary>
    /// Defines the <see cref="AssetAddViewModel" />
    /// </summary>
    public class AssetViewModel
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

    /// <summary>
    /// Defines the <see cref="AssetTreeViewModel" />
    /// </summary>
    public class AssetTreeViewModel : AssetViewModel
    {
        /// <summary>
        /// Gets or sets the Children
        /// </summary>
        public int Children { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="AssetDetailViewModel" />
    /// </summary>
    public class AssetDetailViewModel : AssetViewModel
    {
        /// <summary>
        /// Gets or sets the Children
        /// </summary>
        public string PictureUrl { get; set; }
    }
}
