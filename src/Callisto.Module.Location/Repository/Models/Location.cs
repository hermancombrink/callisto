using Callisto.SharedKernel;
using Callisto.SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Locations.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Location" />
    /// </summary>
    [Table("Locations", Schema = DbConstants.CallistoSchema)]
    public class Location : BaseEfTagModel
    {
        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the Latitude
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the FormattedAddress
        /// </summary>
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the Route
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the Vicinity
        /// </summary>
        public string Vicinity { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the PostCode
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the StateCode
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the UTCOffsetMinutes
        /// </summary>
        public int? UTCOffsetMinutes { get; set; }

        /// <summary>
        /// Gets or sets the GooglePlaceId
        /// </summary>
        public string GooglePlaceId { get; set; }

        /// <summary>
        /// Gets or sets the GoogleURL
        /// </summary>
        public string GoogleURL { get; set; }
    }
}
