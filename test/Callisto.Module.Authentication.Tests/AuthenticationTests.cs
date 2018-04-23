using AutoFixture;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel.Extensions;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Module.Authentication.Tests
{
    /// <summary>
    /// Defines the <see cref="AuthenticationTests" />
    /// </summary>
    public class AuthenticationTests : IClassFixture<WebApiFixture>
    {
        private IFixture Fixture = new Fixture();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationTests"/> class.
        /// </summary>
        /// <param name="webApiFixture">The <see cref="WebApiFixture"/></param>
        public AuthenticationTests(WebApiFixture webApiFixture)
        {
            WebApiFixture = webApiFixture;
        }

        /// <summary>
        /// Gets the WebApiFixture
        /// </summary>
        public WebApiFixture WebApiFixture { get; }

        /// <summary>
        /// The WebApiShouldReturn401WithToken
        /// </summary>
        [Fact]
        public async Task WebApiShouldReturn401WithToken()
        {
            var requestResult = await WebApiFixture.Client.GetAsync("/values");

            requestResult.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiLoginShouldFailWithInvalidCredentials()
        {
            var login = Fixture.Create<LoginViewModel>();
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
