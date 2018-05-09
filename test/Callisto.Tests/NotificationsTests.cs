using AutoFixture;
using Callisto.Module.Notification;
using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
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
            sender.Received(1).SendEmailAsync(Arg.Any<NotificationRequestModel>(), Arg.Any<NotificationType>());
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

            sender.SendEmailAsync(Arg.Any<NotificationRequestModel>(), Arg.Any<NotificationType>()).Returns(c =>
            {
                throw new Exception("Email exception");
            });
            var result = await module.SubmitEmailNotification(model);
            result.Status.Should().Be(RequestStatus.Exception);
            result.FriendlyMessage.Should().Be("That was not suppose to happen");
            result.SystemMessage.Should().Be("Email exception");
            sender.Received(1).SendEmailAsync(Arg.Any<NotificationRequestModel>(), Arg.Any<NotificationType>());
        }

        /// <summary>
        /// The EmailSenderShouldSendNotificationWithSendGrid
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]//(Skip = "Test to see if mail is inboxed")]
        public async Task EmailSenderShouldSendNotificationWithSendGrid()
        {
            var factory = Substitute.For<ISendGridMalFactory>();
            var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                ApiKey = "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",
                FromAddress = "test@test.com",
                FromDisplayName = "Integration Test"
            }), factory);
            factory.CreateMessage(Arg.Any<NotificationType>(), Arg.Any<NotificationRequestModel>()).Returns(c =>
            {
                var mailMessage = new SendGridMessage();
                mailMessage.AddTo("herman.combrink@gmail.com");
                mailMessage.From = new EmailAddress("test@test.com", "Integration Test");
                mailMessage.Subject = "Test Without Template";
                mailMessage.HtmlContent = "Test Body";
                return mailMessage;
            });
            await sender.SendEmailAsync(NotificationRequestModel.Email("herman.combrink@gmail.com", "test subject", "test body"));
        }

        /// <summary>
        /// The EmailSenderShouldFailWithInvalidApiKey
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public void EmailSenderShouldFailWithInvalidApiKey()
        {
            var factory = Substitute.For<ISendGridMalFactory>();
            var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                ApiKey = "xxx",
                FromAddress = "test@test.com",
                FromDisplayName = "Integration Test"
            }), factory);

            factory.CreateMessage(Arg.Any<NotificationType>(), Arg.Any<NotificationRequestModel>()).Returns(c =>
            {
                var mailMessage = new SendGridMessage();
                mailMessage.AddTo("herman.combrink@gmail.com");
                mailMessage.From = new EmailAddress("test@test.com", "Integration Test");
                mailMessage.Subject = "Test Subject";
                mailMessage.HtmlContent = "Test Body";
                return mailMessage;
            });

            Func<Task> act = async () =>
            {
                await sender.SendEmailAsync(NotificationRequestModel.Email("test@test.com", "test subject", "test body"));
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
                var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()), Substitute.For<ISendGridMalFactory>());
                await sender.SendEmailAsync(NotificationRequestModel.Email("test@test.com", "test subject", "test body"));
            };
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SendGridFactoryShouldSetTokensAndTemplateId()
        {
            var factory = new SendGridMailFactory(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                FromAddress = "test@test.com",
                FromDisplayName = "Test",
                Templates = new List<TemplateItem>() {
                    new TemplateItem(){
                        Id = "123",
                        Type = NotificationType.ResetPassword
                    }
                }
            }));
            var message =  NotificationRequestModel.Email("test@client.com", "subject", "message");
            message.AddToken("token", "test");

            var result = factory.CreateMessage(NotificationType.ResetPassword, message);

            result.Personalizations[0].Substitutions.Count.Should().BeGreaterThan(0);
            result.TemplateId.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// The EmailSenderShouldSendNotificationWithSendGrid
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]//(Skip = "Test to see if mail is inboxed")]
        public async Task EmailSenderShouldSendWithTemplate()
        {
            var factory = new SendGridMailFactory(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                FromAddress = "test@test.com",
                FromDisplayName = "Test",
                Templates = new List<TemplateItem>() {
                    new TemplateItem(){
                        Id = "7cc262f1-48eb-4948-a22c-16cb844d4540",
                        Type = NotificationType.ResetPassword
                    }
                }
            }));
            var sender = new SimpleSendGridEmailSender(new OptionsWrapper<MailOptions>(new MailOptions()
            {
                ApiKey = "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",
                FromAddress = "test@test.com",
                FromDisplayName = "Integration Test"
            }), factory);

            var message = NotificationRequestModel.Email("herman.combrink@gmail.com", "subject", "message");
            message.AddToken("-name-", "John Doe");
            message.AddToken("-city-", "Denver");
            message.AddToken("subject", "SendGrid Subject");
            
            await sender.SendEmailAsync(message, NotificationType.ResetPassword);
        }
    }
}
