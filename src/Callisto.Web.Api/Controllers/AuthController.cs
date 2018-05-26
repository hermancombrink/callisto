using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return await CallistoSession.Authentication.ResetPasswordAsync(CallistoSession.UserName);
        }

        /// <summary>
        /// The ForgotPassworod
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpGet("forgot")]
        public async Task<RequestResult> ForgotPassworod(string email)
        {
            return await CallistoSession.Authentication.ResetPasswordAsync(email);
        }

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        [Authorize]
        [HttpGet("user")]
        public async Task<RequestResult<UserViewModel>> GetUser()
        {
            return await CallistoSession.Authentication.GetUserByNameAsync(CallistoSession.UserName);
        }

        [Authorize]
        [HttpPost("user")]
        public async Task<RequestResult> UpdateNewProfile([FromBody] NewAccountViewModel model)
        {
            return await CallistoSession.Authentication.UpdateNewProfileAsync(model);
        }

        /// <summary>
        /// The SignOut
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [Authorize]
        [HttpGet("signout")]
        public async Task<RequestResult> SignOut()
        {
            return await CallistoSession.Authentication.SignOutAsync(CallistoSession.UserName);
        }


    }
}
