using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Customer.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Customer
{
    /// <summary>
    /// Defines the <see cref="ICustomerModule" />
    /// </summary>
    public interface ICustomerModule
    {
        /// <summary>
        /// The AddCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> AddCustomerMember(AddCustomerViewModel model);

        /// <summary>
        /// The RemoveCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> RemoveCustomerMember(Guid Id);

        /// <summary>
        /// The UpdateCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> UpdateCustomerMember();

        /// <summary>
        /// The GetCustomerMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{CustomerViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<CustomerViewModel>>> GetCustomerMembers();

        /// <summary>
        /// The UpdateCurrentMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateCurrentMember(NewAccountViewModel model);
    }
}
