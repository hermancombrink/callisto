using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IAuthenticationRepository" />
    /// </summary>
    public interface IAuthenticationRepository
    {
        /// <summary>
        /// The RegisterNewAccountAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{long}}"/></returns>
        Task<RequestResult<long>> RegisterNewAccountAsync(RegisterViewModel model);

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        Task<RequestResult<UserViewModel>> GetUserByName(string email);

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        Task<IDbContextTransaction> BeginTransaction();

        /// <summary>
        /// The GetCompany
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Company}"/></returns>
        Task<Company> GetCompany(long refId);

        Task UpdateUser(ApplicationUser user);

        Task UpdateCompany(Company company);

        Task<ApplicationUser> GetUser(string userName);
    }
}
