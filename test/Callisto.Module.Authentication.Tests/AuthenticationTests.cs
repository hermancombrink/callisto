using AutoFixture;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Module.Authentication.Tests
{
    /// <summary>
    /// Defines the <see cref="AuthenticationTests" />
    /// </summary>
    public class AuthenticationTests : IClassFixture<WebApiFixture>
    {
        /// <summary>
        /// Defines the Fixture
        /// </summary>
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
        public async Task WebApiLoginShouldFailWithInvalidEmailAddress()
        {
            var login = Fixture.Create<LoginViewModel>();
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await requestResult.Content.ReadAsStringAsync();
            var r = response.FromJson<RequestResult>();
            r.Status.Should().Be(RequestStatus.Warning);
        }


        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiLoginShouldSucceedWithValidCredentials()
        {
            var singup = Fixture.Build<RegisterViewModel>()
               .With(c => c.Email, "test@test.com")
               .With(c => c.Password, "Pass!2")
               .With(c => c.ConfirmPassword, "Pass!2")
               .Create();
             await WebApiFixture.Client.PostAsync("/auth/signup", singup.ToJson().ToContent());

            var login = Fixture.Create<LoginViewModel>();
            singup.CopyProperties(login);
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await requestResult.Content.ReadAsStringAsync();
            var r = response.FromJson<RequestResult>();

            r.Status.Should().Be(RequestStatus.Success);
        }

        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiLoginShouldFailWithNoExistingAccount()
        {
            var login = Fixture.Build<LoginViewModel>()
                .With(c => c.Email, "integrationtest@test.com")
                .Create();
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await requestResult.Content.ReadAsStringAsync();
            var r = response.FromJson<RequestResult>();
            r.Status.Should().Be(RequestStatus.Failed);
        }

        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiRegisterShouldSucceedWithAllIsWell()
        {
            var login = Fixture.Build<RegisterViewModel>()
                .With(c => c.Email, "randomuser@test.com")
                .With(c => c.Password, "Pass!2")
                .With(c => c.ConfirmPassword, "Pass!2")
                .Create();
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/signup", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await requestResult.Content.ReadAsStringAsync();
            var r = response.FromJson<RequestResult>();
            r.FriendlyMessage.Should().Be(string.Empty);
            r.Status.Should().Be(RequestStatus.Success);
        }

        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiRegisterShouldFailWhenAccountExists()
        {
            var user = Fixture.Build<ApplicationUser>()
                 .With(c => c.UserName, "newcustomer@test.com")
                 .With(c => c.Email, "newcustomer@test.com")
                 .Create();
            await WebApiFixture.Context.Users.AddAsync(user);
            await WebApiFixture.Context.SaveChangesAsync();

            var login = Fixture.Build<RegisterViewModel>()
                .With(c => c.Email, "newcustomer@test.com")
                .With(c => c.Password, "Pass!2")
                .With(c => c.ConfirmPassword, "Pass!2")
                .Create();
            var body = login.ToJson().ToContent();
            var requestResult = await WebApiFixture.Client.PostAsync("/auth/signup", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = await requestResult.Content.ReadAsStringAsync();
            var r = response.FromJson<RequestResult>();
            r.Status.Should().Be(RequestStatus.Failed);
        }
    }
}
