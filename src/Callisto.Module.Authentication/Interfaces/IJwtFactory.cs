using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedModels.Auth.ViewModels;

namespace Callisto.Module.Authentication.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IJwtFactory" />
    /// </summary>
    public interface IJwtFactory
    {
        /// <summary>
        /// The GetTokenAsync
        /// </summary>
        /// <param name="userName">The <see cref="string"/></param>
        /// <param name="Id">The <see cref="string"/></param>df
        /// <returns>The <see cref="Task{string}"/></returns>
        string GetToken(UserViewModel user);
    }
}
