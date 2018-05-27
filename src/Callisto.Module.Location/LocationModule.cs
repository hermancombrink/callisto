using Callisto.Module.Locations.Interfaces;
using Callisto.SharedKernel;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Locations
{
    /// <summary>
    /// Defines the <see cref="LocationModule" />
    /// </summary>
    public class LocationModule : ILocationModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationModule"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        /// <param name="logger">The <see cref="ILogger{LocationModule}"/></param>
        /// <param name="locationRepo">The <see cref="ILocationRepository"/></param>
        public LocationModule(
            ICallistoSession session,
            ILogger<LocationModule> logger,
            ILocationRepository locationRepo)
        {
            Session = session;
            Logger = logger;
            LocationRepo = locationRepo;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        private ICallistoSession Session { get; }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<LocationModule> Logger { get; }

        /// <summary>
        /// Gets the LocationRepo
        /// </summary>
        private ILocationRepository LocationRepo { get; }

        /// <summary>
        /// The UpsertCompanyLocation
        /// </summary>
        /// <param name="viewModel">The <see cref="LocationViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult<long>> UpsertLocation(LocationViewModel viewModel)
        {

            var location = await LocationRepo.GetLocationByPlaceId(viewModel.GooglePlaceId, this.Session.CurrentCompanyRef);

            if (location == null)
            {
                location = ModelFactory.CreateLocation(viewModel, this.Session.CurrentCompanyRef);
                await LocationRepo.AddLocation(location);
            }
            else
            {
                ModelFactory.UpdateLocation(location, viewModel);
                await LocationRepo.SaveLocation(location);
            }

            return RequestResult<long>.Success(location.RefId);
        }

        /// <summary>
        /// The GetLocation
        /// </summary>
        /// <param name="locationRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{LocationViewModel}}"/></returns>
        public async Task<RequestResult<LocationViewModel>> GetLocation(long locationRefId)
        {
            var location = await LocationRepo.GetLocationById(locationRefId);
            if (location == null)
            {
                return RequestResult<LocationViewModel>.Validation("Failed to find asset", null);
            }

            return RequestResult<LocationViewModel>.Success(ModelFactory.CreateLocation(location));
        }

        /// <summary>
        /// The GetLocations
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{LocationViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<LocationViewModel>>> GetLocations()
        {

            var list = new List<LocationViewModel>();
            var locations = await LocationRepo.GetLocationsByCompany(Session.CurrentCompanyRef);
            foreach (var item in locations)
            {
                list.Add(ModelFactory.CreateLocation(item));
            }

            return RequestResult<IEnumerable<LocationViewModel>>.Success(list);
        }
    }
}
