using AutoFixture;
using Callisto.Module.Notification.Email;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Notification.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Module.Notification.Tests
{
    /// <summary>
    /// Defines the <see cref="NotificationsTests" />
    /// </summary>
    public class NotificationsTests
    {
        /// <summary>
        /// The Test1
        /// </summary>
        [Fact]
        public async Task SubmitNotificationShouldSucceedWhenAllIsWell()
        {
            var sender = Substitute.For<IEmailSender>();
            var module = new NotificationModule(Substitute.For<ILogger<NotificationModule>>(), sender);

            var fixture = new Fixture();

            var model = fixture.Build<NotificationRequestModel>()
                .Create();

            var result = await module.SubmitEmailNotification(model);
            result.Status.Should().Be(RequestStatus.Success);
            sender.Received(1).SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }


        [Fact]
        public async Task SubmitNotificationShouldBeCriticalWhenSenderFailed()
        {
            var sender = Substitute.For<IEmailSender>();
            var module = new NotificationModule(Substitute.For<ILogger<NotificationModule>>(), sender);

            var fixture = new Fixture();

            var model = fixture.Build<NotificationRequestModel>()
                .Create();

            sender.SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(c =>
            {
                throw new Exception("Email exception");
            });
            var result = await module.SubmitEmailNotification(model);
            result.Status.Should().Be(RequestStatus.Exception);
            result.FriendlyMessage.Should().Be("Oops. That was not suppose to happen");
            result.SystemMessage.Should().Be("Email exception");
            sender.Received(1).SendEmailAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
