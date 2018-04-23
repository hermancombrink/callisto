using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Callisto.Module.Authentication
{
    /// <summary>
    /// Defines the <see cref="AuthenticationModule" />
    /// </summary>
    public class AuthenticationModule : IAuthenticationModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationModule"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{AuthenticationModule}"/></param>
        /// <param name="userManager">The <see cref="UserManager{ApplicationUser}"/></param>
        public AuthenticationModule(
            ILogger<AuthenticationModule> logger,
            IAuthenticationRepository authRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            Logger = logger;
            AuthRepo = authRepo;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<AuthenticationModule> Logger { get; }

        /// <summary>
        /// Gets the UserManager
        /// </summary>
        private UserManager<ApplicationUser> UserManager { get; }

        /// <summary>
        /// Gets the SignInManager
        /// </summary>
        private SignInManager<ApplicationUser> SignInManager { get; }

        /// <summary>
        /// Gets the AuthRepo
        /// </summary>
        private IAuthenticationRepository AuthRepo { get; }

        /// <summary>
        /// The RegisterUserAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RegisterUserAsync(RegisterViewModel model)
        {
            Logger.LogDebug($"Attempting register for {model.Email}");

            if (model is null)
            {
                return RequestResult.Validation($"Request cannot be null");
            }

            if (!model.Validate(out string msg).isValid)
            {
                return RequestResult.Validation(msg);
            }

            using (var tran = new TransactionScope())
            {
                var companyResult = await AuthRepo.RegisterNewAccountAsync(model);
                if (!companyResult.IsSuccess())
                {
                    Logger.LogError($"Failed to regiter componay - {companyResult.SystemMessage}");
                    return companyResult.AsResult;
                }

                var appUser = ModelFactory.CreateUser(model, companyResult.Result);
                var user = await UserManager.CreateAsync(appUser, model.Password);
                if (!user.Succeeded)
                {
                    return RequestResult.Failed($"Failed to create user due to one or more errors [{user.Errors.First()}]");
                }

                tran.Complete();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The LoginUserAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        public async Task<RequestResult> LoginUserAsync(LoginViewModel model)
        {
            if (model is null)
            {
                return RequestResult.Validation($"Request cannot be null");
            }

            if (!model.Validate(out string msg).isValid)
            {
                return RequestResult.Validation(msg);
            }

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //TODO: Move lockout to settings
                //var signInResult = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                var result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RequestResult.Success();
                }
            }

            return RequestResult.Failed($"Login failed for account {model.Email}");
        }
    }
}
