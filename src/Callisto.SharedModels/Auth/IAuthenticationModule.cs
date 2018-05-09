using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Auth
{
    /// <summary>
    /// Defines the <see cref="IAuthenticationModule" />
    /// </summary>
    public interface IAuthenticationModule
    {
        /// <summary>
        /// The RegisterUserAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> RegisterUserAsync(RegisterViewModel model);

        /// <summary>
        /// The LoginUserAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        Task<RequestResult> LoginUserAsync(LoginViewModel model);

        /// <summary>
        /// The ResetPassword
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> ResetPasswordAsync(string email);

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        Task<RequestResult<UserViewModel>> GetUserByNameAsync(string email);

        /// <summary>
        /// The SignOut
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> SignOutAsync(string email);

        /// <summary>
        /// The GetCompanyByRefId
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{CompanyViewModel}}"/></returns>
        Task<RequestResult<CompanyViewModel>> GetCompanyByRefId(long refId);
    }
}
