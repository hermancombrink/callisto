using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Repository
{
    /// <summary>
    /// Defines the <see cref="AuthenticationRepository" />
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRepository"/> class.
        /// </summary>
        /// <param name="contextFactory">The <see cref="Func{ApplicationDbContext}"/></param>
        public AuthenticationRepository(Func<ApplicationDbContext> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private Func<ApplicationDbContext> ContextFactory { get; }

        /// <summary>
        /// The CanRegisterNewCompany
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<RequestResult<long>> RegisterNewAccountAsync(RegisterViewModel model)
        {
            using (var ctx = ContextFactory())
            {
                if (ctx.Users.Any(c => c.UserName == model.Email || c.Email == c.Email))
                {
                    return RequestResult<long>.Failed("User already exists");
                }

                if (ctx.Companies.Any(c => c.Name == model.CompanyName))
                {
                    return RequestResult<long>.Failed("Company already exists");
                }

                var company = ModelFactory.CreateCompany(model);
                await ctx.Companies.AddAsync(company);
                var subscription = ModelFactory.GetSubscription(company);
                await ctx.Subscriptions.AddAsync(subscription);

                await ctx.SaveChangesAsync();

                return RequestResult<long>.Success(company.RefId);
            }
        }
    }
}
