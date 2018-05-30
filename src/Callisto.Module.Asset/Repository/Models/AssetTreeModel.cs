using System;

namespace Callisto.Module.Assets.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="AssetTreeModel" />
    /// </summary>
    public class AssetTreeModel
    {
        /// <summary>
        /// Gets or sets the RefId
        /// </summary>
        public long RefId { get; set; }

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

        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the Children
        /// </summary>
        public int Children { get; set; }

        /// <summary>
        /// Gets or sets the ParentRefId
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
