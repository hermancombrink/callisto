using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
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
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public AuthController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        [HttpPost("login")]
        public async Task<RequestResult> LoginAsync([FromBody] LoginViewModel model)
        {
            return await CallistoSession.Authentication.LoginUserAsync(model);
        }

        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        [HttpPost("signup")]
        public async Task<RequestResult> SignUpAsync([FromBody] RegisterViewModel model)
        {
            return await CallistoSession.Authentication.RegisterUserAsync(model);
        }

        /// <summary>
        /// The ResetPassworod
        /// </summary>
        /// <param name="model">The <see cref="ResetPasswordViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [Authorize]
        [HttpGet("reset")]
        public async Task<RequestResult> ResetPassworod()
        {
            return await CallistoSession.Authentication.ResetPassword(CallistoSession.UserName);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<RequestResult<UserViewModel>> GetUser()
        {
            return await CallistoSession.Authentication.GetUserByName(CallistoSession.UserName);
        }
    }
}
