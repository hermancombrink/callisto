using Callisto.Module.Authentication.Repository;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedModels.Session;
using Callisto.Tests.Startups;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Callisto.Tests.Fixtures
{
    /// <summary>
    /// Defines the <see cref="AuthApiFixture" />
    /// </summary>
    public class AuthApiFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthApiFixture"/> class.
        /// </summary>
        public AuthApiFixture()
        {
            var host = WebHost.CreateDefaultBuilder()
                      .UseStartup<AuthSartup>();

            Server = new TestServer(host);
            
            Context = Server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            UserManager = Server.Host.Services.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            SignInManger = Server.Host.Services.GetService(typeof(SignInManager<ApplicationUser>)) as SignInManager<ApplicationUser>;
            Session = Server.Host.Services.GetService(typeof(ICallistoSession)) as ICallistoSession;
            Client = Server.CreateClient();
            Services = Server.Host.Services;
        }

        /// <summary>
        /// Gets the Server
        /// </summary>
        public TestServer Server { get; }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public ApplicationDbContext Context { get; }

        /// <summary>
        /// Gets the UserMamanger
        /// </summary>
        public UserManager<ApplicationUser> UserManager { get; }

        /// <summary>
        /// Gets the SignInManger
        /// </summary>
        public SignInManager<ApplicationUser> SignInManger { get; }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// The GetService
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="T"/></returns>
        public T GetService<T>()
        {
            return (T)Server.Host.Services.GetService(typeof(T));
        }

        /// <summary>
        /// Gets the Client
        /// </summary>
        public HttpClient Client { get; }

        /// <summary>
        /// Gets the Services
        /// </summary>
        public IServiceProvider Services { get; }
    }
}
