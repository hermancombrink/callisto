using Callisto.SharedKernel;
using Callisto.SharedModels.Location.ViewModels;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Location
{
    /// <summary>
    /// Defines the <see cref="ILocationModule" />
    /// </summary>
    public interface ILocationModule
    {
        /// <summary>
        /// The UpsertCompanyLocation
        /// </summary>
        /// <param name="viewModel">The <see cref="LocationViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult<long>> UpsertLocation(LocationViewModel viewModel);

        /// <summary>
        /// The GetLocation
        /// </summary>
        /// <param name="locationRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{LocationViewModel}}"/></returns>
        Task<RequestResult<LocationViewModel>> GetLocation(long locationRefId);
    }
}
