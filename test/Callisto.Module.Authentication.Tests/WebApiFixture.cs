using Callisto.Module.Authentication.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace Callisto.Module.Authentication.Tests
{
    /// <summary>
    /// Defines the <see cref="WebApiFixture" />
    /// </summary>
    public class WebApiFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiFixture"/> class.
        /// </summary>
        public WebApiFixture()
        {
            var host = new WebHostBuilder()
                      .UseStartup<TestStartup>();

            Server = new TestServer(host);
            Context = Server.Host.Services.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            Client = Server.CreateClient();
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
        /// Gets the Client
        /// </summary>
        public HttpClient Client { get; }
    }
}
