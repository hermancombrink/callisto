using Callisto.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Assets.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="AssetLocation" />
    /// </summary>
    [Table("AssetLocations", Schema = DbConstants.CallistoSchema)]
    public class AssetLocation
    {
        /// <summary>
        /// Gets or sets the AssetRefId
        /// </summary>
        [Key]
        public long AssetRefId { get; set; }

        /// <summary>
        /// Gets or sets the LocationRefId
        /// </summary>
        [Key]
        public long LocationRefId { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedAt
        /// </summary>
        public DateTime? ModifiedAt { get; set; }
    }
}
