﻿using Callisto.SharedKernel;
using Callisto.SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Assets.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Asset" />
    /// </summary>
    [Table("Assets", Schema = DbConstants.CallistoSchema)]
    public class Asset : BaseEfModel
    {
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
    }
}