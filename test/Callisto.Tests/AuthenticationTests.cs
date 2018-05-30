using AutoFixture;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using Callisto.Tests.Fixtures;
using FluentAssertions;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="AuthenticationTests" />
    /// </summary>
    public class AuthenticationTests : IClassFixture<AuthApiFixture>
    {
        /// <summary>
        /// Defines the Fixture
        /// </summary>
        private IFixture Fixture = new Fixture();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationTests"/> class.
        /// </summary>
        /// <param name="ApiFixture">The <see cref="ApiFixture"/></param>
        public AuthenticationTests(AuthApiFixture apiFixture)
        {
            ApiFixture = apiFixture;
        }

        /// <summary>
        /// Gets the ApiFixture
        /// </summary>
        public AuthApiFixture ApiFixture { get; }

        /// <summary>
        /// The WebApiLoginShouldFailWithInvalidCredentials
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiLoginShouldFailWithInvalidEmailAddress()
        {
            var login = Fixture.Create<LoginViewModel>();
            var body = login.ToJson().ToContent();
            var requestResult = await ApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = requestResult.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Warning);
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
            await ApiFixture.Client.PostAsync("/auth/signup", singup.ToJson().ToContent());

            var login = Fixture.Create<LoginViewModel>();
            singup.CopyProperties(login);
            var body = login.ToJson().ToContent();
            var requestResult = await ApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = requestResult.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Success);
            response.Result.Should().NotBeNullOrEmpty();
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
            var requestResult = await ApiFixture.Client.PostAsync("/auth/login", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = requestResult.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Failed);
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
            var requestResult = await ApiFixture.Client.PostAsync("/auth/signup", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = requestResult.ToRequestResult();
            response.FriendlyMessage.Should().Be(string.Empty);
            response.Status.Should().Be(RequestStatus.Success);
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
            await ApiFixture.Context.Users.AddAsync(user);
            await ApiFixture.Context.SaveChangesAsync();

            var login = Fixture.Build<RegisterViewModel>()
                .With(c => c.Email, "newcustomer@test.com")
                .With(c => c.Password, "Pass!2")
                .With(c => c.ConfirmPassword, "Pass!2")
                .Create();
            var body = login.ToJson().ToContent();
            var requestResult = await ApiFixture.Client.PostAsync("/auth/signup", body);

            requestResult.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = requestResult.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Failed);
        }

        /// <summary>
        /// The ResetPasswordShouldReturnTokenWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task ResetPasswordShouldReturnTokenWhenAllIsWell()
        {
            var user = new ApplicationUser()
            {
                Email = "reset@test.com",
                UserName = "reset@test.com",
                FirstName = "test",
                LastName = "test",
            };

            var create = await ApiFixture.UserManager.CreateAsync(user, "Password!2");

            var login = new LoginViewModel()
            {
                Email = "reset@test.com",
                Password = "Password!2"
            };

            var signin = await ApiFixture.Client.PostAsync("/auth/login", login.ToJson().ToContent());
            var r = signin.ToRequestResult();

            var notification = ApiFixture.GetService<INotificationModule>();
            notification.SubmitEmailNotification(Arg.Any<NotificationRequestModel>(), Arg.Any<NotificationType>()).Returns(c =>
            {
                return RequestResult.Success();
            });
            var client = ApiFixture.Server.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{r.Result}");


            var reset = await client.GetAsync("/auth/reset");
            reset.EnsureSuccessStatusCode();
            var response = reset.ToRequestResult();
            response.Status.Should().Be(RequestStatus.Success);

            notification.Received(1).SubmitEmailNotification(Arg.Any<NotificationRequestModel>(), Arg.Any<NotificationType>());
        }

        /// <summary>
        /// The WebApiShouldReturn401WithToken
        /// </summary>
        [Fact]
        public async Task WebApiShouldReturn401WithoutToken()
        {
            var reset = await ApiFixture.Client.GetAsync("/auth/reset");

            reset.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// The WebApiShouldReturn401WithToken
        /// </summary>
        [Fact]
        public async Task WebApiShouldReturn401WithInvalidToken()
        {
            var client = ApiFixture.Server.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"wakka wakka");

            var reset = await client.GetAsync("/auth/reset");

            reset.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// The WebApiUserDetailsShouldReturnWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiUserDetailsShouldReturnWhenAllIsWell()
        {
            await ApiFixture.Session.Authentication.RegisterUserAsync(new RegisterViewModel()
            {
                ConfirmPassword = "Pass!2",
                Password = "Pass!2",
                Email = "newuser@test.com",
            });

            var login = new LoginViewModel()
            {
                Email = "newuser@test.com",
                Password = "Pass!2"
            };

            var signin = await ApiFixture.Client.PostAsync("/auth/login", login.ToJson().ToContent());
            var r = signin.ToRequestResult();

            var client = ApiFixture.Server.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{r.Result}");

            var result = await client.GetAsync("/auth/user");

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var uvm = result.ToRequestResult<UserViewModel>();
            uvm.Result.Company.Should().Be(string.Empty);
            uvm.Result.SubscriptionId.Should().NotBeEmpty();
        }

        /// <summary>
        /// The WebApiUserDetailsShouldBeUnauthorized
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task WebApiUserDetailsShouldBeUnauthorized()
        {

            var client = ApiFixture.Server.CreateClient();

            var result = await client.GetAsync("/auth/user");

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
