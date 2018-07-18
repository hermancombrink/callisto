using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Base;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IAuthenticationRepository" />
    /// </summary>
    public interface IAuthenticationRepository : IBaseRepository
    {
        /// <summary>
        /// The CreateCompany
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task CreateCompany(Company company);

        /// <summary>
        /// The CreateSubscription
        /// </summary>
        /// <param name="subscription">The <see cref="Subscription"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task CreateSubscription(Subscription subscription);

        /// <summary>
        /// The UserExists
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        Task<bool> UserExists(string email);

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        Task<RequestResult<UserViewModel>> GetUserByName(string email);

        /// <summary>
        /// The GetCompany
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Company}"/></returns>
        Task<Company> GetCompany(long refId);

        /// <summary>
        /// The UpdateUser
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdateUser(ApplicationUser user);

        /// <summary>
        /// The UpdateCompany
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdateCompany(Company company);

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <param name="userName">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> GetUser(string userName);

        /// <summary>
        /// The GetUserById
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{ApplicationUser}"/></returns>
        Task<ApplicationUser> GetUserById(string userId);

        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveAccount(ApplicationUser user);

        /// <summary>
        /// The GetSubscription
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Subscription}"/></returns>
        Task<Subscription> GetSubscription(string userId, long companyRefId);

        /// <summary>
        /// The UpdateSubscription
        /// </summary>
        /// <param name="subscriptions">The <see cref="Subscription"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdateSubscription(Subscription subscriptions);

        /// <summary>
        /// The RemoveSubscription
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveSubscription(ApplicationUser user, long companyRefId);

        /// <summary>
        /// The HasSubscription
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <param name="type">The <see cref="UserType"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task<bool> HasSubscription(string userId, UserType type, long companyRefId);
    }
}
