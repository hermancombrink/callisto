using Callisto.SharedModels.Session;
using Callisto.Tests.Startups;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="WebApiFixture" />
    /// </summary>
    public class SessionApiFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebApiFixture"/> class.
        /// </summary>
        public SessionApiFixture()
        {
            var host = new WebHostBuilder()
                      .UseStartup<SessionStartup>();

            Server = new TestServer(host);
            Client = Server.CreateClient();
            Services = Server.Host.Services;
            Session = Server.Host.Services.GetService(typeof(ICallistoSession)) as ICallistoSession;

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

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }
    }
}
