using Callisto.Module.Locations.Repository.Models;
using Callisto.SharedModels.Location.ViewModels;
using System;

namespace Callisto.Module.Locations
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateLocation
        /// </summary>
        /// <param name="viewModel">The <see cref="LocationViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Location"/></returns>
        public static Location CreateLocation(LocationViewModel viewModel, long companyRefId)
        {
            return new Location()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                City = viewModel.City,
                Country = viewModel.Country,
                CountryCode = viewModel.CountryCode,
                FormattedAddress = viewModel.FormattedAddress,
                GooglePlaceId = viewModel.GooglePlaceId,
                GoogleURL = viewModel.GoogleURL,
                Latitude = viewModel.Latitude,
                Longitude = viewModel.Longitude,
                PostCode = viewModel.PostCode,
                Route = viewModel.Route,
                State = viewModel.State,
                StateCode = viewModel.StateCode,
                UTCOffsetMinutes = viewModel.UTCOffsetMinutes,
                Vicinity = viewModel.Vicinity
            };
        }

        /// <summary>
        /// The UpdateLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <param name="viewModel">The <see cref="LocationViewModel"/></param>
        public static void UpdateLocation(Location location, LocationViewModel viewModel)
        {
            location.ModifiedAt = DateTime.Now;
            location.City = viewModel.City;
            location.Country = viewModel.Country;
            location.CountryCode = viewModel.CountryCode;
            location.FormattedAddress = viewModel.FormattedAddress;
            location.GooglePlaceId = viewModel.GooglePlaceId;
            location.GoogleURL = viewModel.GoogleURL;
            location.Latitude = viewModel.Latitude;
            location.Longitude = viewModel.Longitude;
            location.PostCode = viewModel.PostCode;
            location.Route = viewModel.Route;
            location.State = viewModel.State;
            location.StateCode = viewModel.StateCode;
            location.UTCOffsetMinutes = viewModel.UTCOffsetMinutes;
            location.Vicinity = viewModel.Vicinity;
        }

        /// <summary>
        /// The CreateLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="LocationViewModel"/></returns>
        public static LocationViewModel CreateLocation(Location location)
        {
            return new LocationViewModel()
            {
                Id = location.Id,
                City = location.City,
                Country = location.Country,
                CountryCode = location.CountryCode,
                FormattedAddress = location.FormattedAddress,
                GooglePlaceId = location.GooglePlaceId,
                GoogleURL = location.GoogleURL,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                PostCode = location.PostCode,
                Route = location.Route,
                State = location.State,
                StateCode = location.StateCode,
                UTCOffsetMinutes = location.UTCOffsetMinutes,
                Vicinity = location.Vicinity
            };
        }
    }
}
