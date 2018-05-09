using Callisto.SharedKernel;
using Callisto.SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Authentication.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Company" />
    /// </summary>
    [Table("Companies", Schema = DbConstants.CallistoSchema)]
    public class Company : BaseEfModel
    {
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
