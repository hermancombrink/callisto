using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="AuthController" />
    /// </summary>
    [Produces("application/json")]
    [Route("auth")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authenticationModule">The <see cref="IAuthenticationModule"/></param>
        public AuthController(IAuthenticationModule authenticationModule)
        {
            AuthenticationModule = authenticationModule;
        }

        /// <summary>
        /// Gets the AuthenticationModule
        /// </summary>
        public IAuthenticationModule AuthenticationModule { get; }

        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        [HttpPost("login")]
        public async Task<RequestResult<string>> LoginAsync([FromBody] LoginViewModel model)
        {
            return await Task.FromResult(RequestResult<string>.Failed("Failed to login"));
        }
    }
}
