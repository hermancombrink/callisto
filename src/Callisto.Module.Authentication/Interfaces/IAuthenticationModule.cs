using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Interfaces
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
    }
}
