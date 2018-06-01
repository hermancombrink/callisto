using Callisto.SharedKernel;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Staff.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="LocationController" />
    /// </summary>
    [Produces("application/json")]
    [Route("staff")]
    [Authorize]
    public class StaffController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public StaffController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The CreateStaffMember
        /// </summary>
        /// <param name="model">The <see cref="AddStaffViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost]
        public async Task<RequestResult> CreateStaffMember([FromBody] AddStaffViewModel model)
        {
            return await CallistoSession.Staff.AddStaffMember(model);
        }

        /// <summary>
        /// The GetStaffMember
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpGet]
        public async Task<RequestResult<IEnumerable<StaffViewModel>>> GetStaffMember()
        {
            return await CallistoSession.Staff.GetStaffMembers();
        }
    }
}
