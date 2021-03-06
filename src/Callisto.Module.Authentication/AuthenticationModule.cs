﻿using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            ICallistoSession session,
            ILogger<AuthenticationModule> logger,
            IAuthenticationRepository authRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            Session = session;
            Logger = logger;
            AuthRepo = authRepo;
            UserManager = userManager;
            SignInManager = signInManager;
            JwtFactory = jwtFactory;
            JwtOptions = jwtOptions?.Value ?? throw new ArgumentException(nameof(jwtOptions));
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        private ICallistoSession Session { get; }

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
        /// Gets the JwtFactory
        /// </summary>
        public IJwtFactory JwtFactory { get; }

        /// <summary>
        /// Gets the JwtOptions
        /// </summary>
        public JwtIssuerOptions JwtOptions { get; }

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
            return await RequestResult.From(async () =>
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

                 using (var tran = await AuthRepo.BeginTransaction())
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
                         return RequestResult.Failed(string.Join("<br/>", user.Errors.Select(c => c.Description)));
                     }

                     tran.Commit();
                 }

                 return RequestResult.Success();
             });
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
                    var token = JwtFactory.GetToken(model.Email, user.Id);
                    return RequestResult.Success(token);
                }
            }

            return RequestResult.Failed($"Login failed for account {model.Email}");
        }

        /// <summary>
        /// The ResetPassword
        /// </summary>
        /// <param name="model">The <see cref="ResetPasswordViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> ResetPassword(string email)
        {
            return await RequestResult.From(async () =>
            {
                if (string.IsNullOrEmpty(email))
                {
                    return RequestResult.Failed($"Email cannot be empty");
                }

                var user = await UserManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);

                    var result = await Session.Notification.SubmitEmailNotification(NotificationRequestModel.Email(email,
                         "Your password has been reset",
                         $"Reset token - [{token}]").AddToken("~token~", token), NotificationType.ResetPassword);

                    return result;
                }

                return RequestResult.Failed($"Failed to find login for account {email}");
            });
        }

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{UserViewModel}"/></returns>
        public async Task<RequestResult<UserViewModel>> GetUserByName(string email)
        {
            return await AuthRepo.GetUserByName(email);
        }

        /// <summary>
        /// The SignOut
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SignOut(string email)
        {
            await SignInManager.SignOutAsync();
            return RequestResult.Success();
        }
    }
}
