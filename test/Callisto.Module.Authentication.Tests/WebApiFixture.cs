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

            Client = Server.CreateClient();
        }

        /// <summary>
        /// Gets the Server
        /// </summary>
        public TestServer Server { get; }

        /// <summary>
        /// Gets the Client
        /// </summary>
        public HttpClient Client { get; }
    }
}
