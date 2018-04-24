﻿using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
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
        public async Task<RequestResult> LoginAsync([FromBody] LoginViewModel model)
        {
            return await AuthenticationModule.LoginUserAsync(model);
        }

        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        [HttpPost("signup")]
        public async Task<RequestResult> SignUpAsync([FromBody] RegisterViewModel model)
        {
            return await AuthenticationModule.RegisterUserAsync(model);
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
            return await AuthenticationModule.ResetPassword(HttpContext.User.Identity.Name);
        }
    }
}
