using AutoFixture;
using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Notification.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        /// <summary>
        /// The SubmitNotificationShouldBeCriticalWhenSenderFailed
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

#pragma warning disable xUnit1004 // Test methods should not be skipped
        /// <summary>
        /// The EmailSenderShouldSendNotificationWithSendGrid
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact(Skip = "Test to see if mail is inboxed")]
#pragma warning restore xUnit1004 // Test methods should not be skipped
        public async Task EmailSenderShouldSendNotificationWithSendGrid()
        {
            var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                ApiKey = "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",
                FromAddress = "test@test.com",
                FromDisplayName = "Integration Test"
            }));

            await sender.SendEmailAsync("herman.combrink@gmail.com", "test subject", "test body");
        }

        /// <summary>
        /// The EmailSenderShouldFailWithInvalidApiKey
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task EmailSenderShouldFailWithInvalidApiKey()
        {
            var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                ApiKey = "xxx",
                FromAddress = "test@test.com",
                FromDisplayName = "Integration Test"
            }));

            Func<Task> act = async () =>
            {
                await sender.SendEmailAsync("test@test.com", "test subject", "test body");
            };
            act.Should().Throw<InvalidOperationException>();
        }

        /// <summary>
        /// The EmailSenderShouldThrowWhenOptionsNotConfigured
        /// </summary>
        [Fact]
        public void EmailSenderShouldThrowWhenOptionsNotConfigured()
        {

            Func<Task> act = async () =>
            {
                var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()));
                await sender.SendEmailAsync("test@test.com", "test subject", "test body");
            };
            act.Should().Throw<ArgumentException>();
        }
    }
}
