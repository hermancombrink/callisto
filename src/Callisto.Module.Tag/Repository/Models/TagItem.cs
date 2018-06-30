using Callisto.SharedKernel;
using Callisto.SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Tag.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Location" />
    /// </summary>
    [Table("Tags", Schema = DbConstants.CallistoSchema)]
    public class TagItem : BaseEfModel
    {
        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }
    }
}
