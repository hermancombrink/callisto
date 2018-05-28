using System;

namespace Callisto.SharedModels.Location.ViewModels
{
    /// <summary>
    /// Defines the <see cref="LocationViewModel" />
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

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
