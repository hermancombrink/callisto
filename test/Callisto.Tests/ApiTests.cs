using App.Metrics.Health;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="ApiTests" />
    /// </summary>
    public class ApiTests : IClassFixture<SessionApiFixture>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiTests"/> class.
        /// </summary>
        /// <param name="webApiFixture">The <see cref="WebApiFixture"/></param>
        public ApiTests(SessionApiFixture webApiFixture)
        {
            ApiFixture = webApiFixture;
        }

        /// <summary>
        /// Gets the WebApiFixture
        /// </summary>
        public SessionApiFixture ApiFixture { get; }

        /// <summary>
        /// The ApiShouldStartUpWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task ApiShouldStartUpWhenAllIsWell()
        {
            var client = ApiFixture.Client;
            var reset = await client.GetAsync("/auth/forgot");

            reset.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = reset.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Failed);
        }

        [Fact]
        public void ApiShouldHaveAllDependantServiceRegisteredForCallisto()
        {
            foreach (var property in ApiFixture.Session.GetType().GetProperties())
            {
                property.GetValue(ApiFixture.Session).Should().NotBeNull();
            }
                
        }

        [Theory]
        [InlineData("/metrics")]
        [InlineData("/metrics-text")]
        [InlineData("/health")]
        [InlineData("/ping")]
        [InlineData("/env")]
        public async Task ApiAllMetricsShouldBeEnabled(string endpoint)
        {
            var client = ApiFixture.Client;
            var reset = await client.GetAsync(endpoint);

            reset.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
