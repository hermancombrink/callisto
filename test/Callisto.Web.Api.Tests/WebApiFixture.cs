using Callisto.Module.Authentication.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Callisto.Web.Api.Tests
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
            Client = Server.CreateClient();
            Services = Server.Host.Services;
        }

        /// <summary>
        /// Gets the Server
        /// </summary>
        public TestServer Server { get; }

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
