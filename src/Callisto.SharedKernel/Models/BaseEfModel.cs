using System;
using System.ComponentModel.DataAnnotations;

namespace Callisto.SharedKernel.Models
{
    /// <summary>
    /// Defines the <see cref="BaseEfModel" />
    /// </summary>
    public abstract class BaseEfModel
    {
        /// <summary>
        /// Gets or sets the RefId
        /// </summary>
        [Key]
        public long RefId { get; set; }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedAt
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
