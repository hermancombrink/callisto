using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Base;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Auth
{
    /// <summary>
    /// Defines the <see cref="IAuthenticationModule" />
    /// </summary>
    public interface IAuthenticationModule : IBaseModule
    {
        /// <summary>
        /// The RegisterUserAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult<(string userId, long companyRefId)>> RegisterUserAsync(RegisterViewModel model);

        /// <summary>
        /// The RegisterUserWithCurrentCompanyAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> RegisterUserWithCurrentCompanyAsync(RegisterViewModel model);

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

        /// <summary>
        /// The UpdateNewProfileAsync
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateNewProfileAsync(NewAccountViewModel model);

        /// <summary>
        /// The LoginWithSocialAsync
        /// </summary>
        /// <param name="model">The <see cref="SocialLoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> LoginWithSocialAsync(SocialLoginViewModel model);

        /// <summary>
        /// The GetUserId
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> GetUserId(string email);

        /// <summary>
        /// The ResetAccount
        /// </summary>
        /// <param name="model">The <see cref="ConfirmAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> ResetAccount(ConfirmAccountViewModel model);

        /// <summary>
        /// The ConfirmAccount
        /// </summary>
        /// <param name="model">The <see cref="ConfirmAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> ConfirmAccount(ConfirmAccountViewModel model);

        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveAccount(string email);

        /// <summary>
        /// The GenerateRandomPassword
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string GenerateRandomPassword();
    }
}
