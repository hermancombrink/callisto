using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
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
    }
}
