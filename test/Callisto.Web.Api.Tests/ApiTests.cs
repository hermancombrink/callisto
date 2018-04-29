using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Web.Api.Tests
{
    /// <summary>
    /// Defines the <see cref="ApiTests" />
    /// </summary>
    public class ApiTests : IClassFixture<WebApiFixture>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiTests"/> class.
        /// </summary>
        /// <param name="webApiFixture">The <see cref="WebApiFixture"/></param>
        public ApiTests(WebApiFixture webApiFixture)
        {
            WebApiFixture = webApiFixture;
        }

        /// <summary>
        /// Gets the WebApiFixture
        /// </summary>
        public WebApiFixture WebApiFixture { get; }

        /// <summary>
        /// The ApiShouldStartUpWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task ApiShouldStartUpWhenAllIsWell()
        {
            var client = WebApiFixture.Client;
            var reset = await client.GetAsync("/auth/forgot");

            reset.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = reset.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Failed);
        }

        [Fact]
        public void ApiShouldHaveAllDependantServiceRegisteredForCallisto()
        {
            foreach (var property in WebApiFixture.Session.GetType().GetProperties())
            {
                property.GetValue(WebApiFixture.Session).Should().NotBeNull();
            }
                
        }

        [Theory]
        [InlineData("/metrics")]
        [InlineData("/metrics-text")]
        [InlineData("/health")]
        [InlineData("/ping")]
        public async Task ApiAllMetricsShouldBeEnabled(string endpoint)
        {
            var client = WebApiFixture.Client;
            var reset = await client.GetAsync(endpoint);

            reset.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
