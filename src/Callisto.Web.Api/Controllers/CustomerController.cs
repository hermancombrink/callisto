using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Customer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="LocationController" />
    /// </summary>
    [Produces("application/json")]
    [Route("customer")]
    [Authorize]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public CustomerController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The CreateCustomerMember
        /// </summary>
        /// <param name="model">The <see cref="AddCustomerViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost]
        public async Task<RequestResult> CreateCustomerMember([FromBody] AddCustomerViewModel model)
        {
            return await CallistoSession.Customer.AddCustomerMember(model);
        }

        /// <summary>
        /// The GetCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpGet]
        public async Task<RequestResult<IEnumerable<CustomerViewModel>>> GetCustomerMember()
        {
            return await CallistoSession.Customer.GetCustomerMembers();
        }

        /// <summary>
        /// The RemoveCustomerMember
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<RequestResult> RemoveCustomerMember(Guid id)
        {
            return await CallistoSession.Customer.RemoveCustomerMember(id);
        }

        /// <summary>
        /// The UpdateNewProfile
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost("profile")]
        public async Task<RequestResult> UpdateNewProfile([FromBody] NewAccountViewModel model)
        {
            return await CallistoSession.Customer.UpdateCurrentMember(model);
        }
    }
}
