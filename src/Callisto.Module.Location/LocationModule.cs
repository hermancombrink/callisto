using Callisto.Module.Locations.Interfaces;
using Callisto.SharedKernel;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;
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
        public async Task<RequestResult<long>> UpsertCompanyLocation(LocationViewModel viewModel)
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
    }
}
