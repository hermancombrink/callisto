using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
            Logger.LogDebug($"Attempting register for {model.Email}");

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
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

            var message = Session.Notification.CreateSimpleMessage(NotificationType.Registered, model.Email);

            Session.MessageCoordinator.Publish(message, Session);

            return RequestResult.Success();
        }

        /// <summary>
        /// The RegisterUserWithCurrentCompanyAsync
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> RegisterUserWithCurrentCompanyAsync(RegisterViewModel model)
        {
            Logger.LogDebug($"Attempting register for {model.Email}");

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!model.Validate(out string msg).isValid)
            {
                return RequestResult.Validation(msg);
            }

            using (var tran = await AuthRepo.BeginTransaction())
            {
                var appUser = ModelFactory.CreateUser(model, Session.CurrentCompanyRef);
                var user = await UserManager.CreateAsync(appUser, model.Password);
                if (!user.Succeeded)
                {
                    return RequestResult.Failed(string.Join("<br/>", user.Errors.Select(c => c.Description)));
                }

                appUser = await AuthRepo.GetUser(model.Email);

                var token = await UserManager.GeneratePasswordResetTokenAsync(appUser);

                tran.Commit();

                return RequestResult.Success(token);
            }
        }

        /// <summary>
        /// The LoginWithSocialAsync
        /// </summary>
        /// <param name="model">The <see cref="SocialLoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> LoginWithSocialAsync(SocialLoginViewModel model)
        {
            var user = await AuthRepo.GetUser(model.Email);

            if (user == null)
            {
                var registerResult = await RegisterUserAsync(ModelFactory.CreateRegistration(model, GenerateRandomPassword()));
                if (registerResult.Status != RequestStatus.Success)
                {
                    throw new InvalidOperationException($"Failed to register user with social name");
                }

                user = await AuthRepo.GetUser(model.Email);
            }

            var logins = await UserManager.GetLoginsAsync(user);
            var currentLogin = logins.FirstOrDefault(c => c.ProviderKey == model.Provider);
            if (currentLogin == null)
            {
                var createResult = await UserManager.AddLoginAsync(user, new UserLoginInfo(model.Provider, model.Token, model.Name));
                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException($"Failed to add login for user");
                }
            }

            var result = await SignInManager.ExternalLoginSignInAsync(model.Provider, model.Token, false);
            if (result.Succeeded)
            {
                var token = JwtFactory.GetToken(user);
                return RequestResult.Success(token);
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
            if (user != null && !user.Deactivated)
            {
                //TODO: Move lockout to settings
                //var signInResult = await SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                var result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var token = JwtFactory.GetToken(user);
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
        public async Task<RequestResult> ResetPasswordAsync(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return RequestResult.Failed($"Email cannot be empty");
            }

            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);

                var message = Session.Notification.CreateSimpleMessage(NotificationType.ResetPassword, email, token.ToTokenDictionary("~token~"));

                Session.MessageCoordinator.Publish(message, Session);

                return RequestResult.Success();
            }

            return RequestResult.Failed($"Failed to find login for account {email}");
        }

        /// <summary>
        /// The ConfirmAccount
        /// </summary>
        /// <param name="model">The <see cref="ConfirmAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> ConfirmAccount(ConfirmAccountViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!model.Validate(out string msg).isValid)
            {
                return RequestResult.Validation(msg);
            }

            var user = await AuthRepo.GetUser(model.Email);

            if (user == null)
            {
                throw new InvalidOperationException($"Failed to find user");
            }

            using (var tran = await AuthRepo.BeginTransaction())
            {
                var reset = await UserManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (!reset.Succeeded)
                {
                    Logger.LogWarning($"Reset errors {string.Join(",", reset.Errors)}");
                    return RequestResult.Failed(string.Join("<br/>", reset.Errors.Select(c => c.Description)));
                }

                var confirmToken = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                var confirm = await UserManager.ConfirmEmailAsync(user, confirmToken);

                var unclock = await UserManager.SetLockoutEnabledAsync(user, false);

                if (!confirm.Succeeded || !unclock.Succeeded)
                {
                    Logger.LogWarning($"Confirmation errors {string.Join(",", confirm.Errors)}");
                    Logger.LogWarning($"Unlock errors {string.Join(",", unclock.Errors)}");
                    throw new InvalidOperationException($"Failed to validate account");
                }

                tran.Commit();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The ResetAccount
        /// </summary>
        /// <param name="model">The <see cref="ConfirmAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> ResetAccount(ConfirmAccountViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!model.Validate(out string msg).isValid)
            {
                return RequestResult.Validation(msg);
            }

            var user = await AuthRepo.GetUser(model.Email);

            if (user == null)
            {
                throw new InvalidOperationException($"Failed to find user");
            }

            var reset = await UserManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!reset.Succeeded)
            {
                Logger.LogWarning($"Reset errors {string.Join(",", reset.Errors)}");
                return RequestResult.Failed(string.Join("<br/>", reset.Errors.Select(c => c.Description)));
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{UserViewModel}"/></returns>
        public async Task<RequestResult<UserViewModel>> GetUserByNameAsync(string email)
        {
            return await AuthRepo.GetUserByName(email);
        }

        /// <summary>
        /// The GetUserId
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> GetUserId(string email)
        {
            var user = await AuthRepo.GetUser(email);
            if (user == null)
            {
                return RequestResult.Failed($"User not found");
            }

            return RequestResult.Success(user.Id);
        }

        /// <summary>
        /// The SignOut
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SignOutAsync(string email)
        {
            await SignInManager.SignOutAsync();
            return RequestResult.Success();
        }

        /// <summary>
        /// The GetCompanyByRefId
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{CompanyViewModel}}"/></returns>
        public async Task<RequestResult<CompanyViewModel>> GetCompanyByRefId(long refId)
        {
            var company = await AuthRepo.GetCompany(refId);

            if (company == null)
            {
                throw new InvalidOperationException($"Unable to find company");
            }

            return RequestResult<CompanyViewModel>.Success(ModelFactory.CreateCompany(company));
        }

        /// <summary>
        /// The UpdateNewProfileAsync
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> UpdateNewProfileAsync(NewAccountViewModel model)
        {
            var user = await AuthRepo.GetUser(Session.UserName);
            if (user == null)
            {
                throw new InvalidOperationException($"Failed to find user");
            }

            var company = await this.AuthRepo.GetCompany(this.Session.CurrentCompanyRef);
            if (company == null)
            {
                throw new InvalidOperationException($"Failed to find company");
            }

            ModelFactory.UpdateNewUserDetails(user, company, model);

            await AuthRepo.UpdateUser(user);

            await AuthRepo.UpdateCompany(company);

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveAccount(string email)
        {
            var user = await AuthRepo.GetUser(email);
            if (user != null)
            {
                await AuthRepo.RemoveAccount(user);
            }
        }

        /// <summary>
        /// The GenerateRandomPassword
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        public string GenerateRandomPassword()
        {
            var opts = UserManager.Options?.Password;
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
