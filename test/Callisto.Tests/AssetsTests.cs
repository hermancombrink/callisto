using AutoFixture;
using Callisto.Module.Assets;
using Callisto.Module.Assets.Interfaces;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Session;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="AssetsTests" />
    /// </summary>
    public class AssetsTests
    {
        /// <summary>
        /// The AddAssestShouldReturnSuccessWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddAssestShouldReturnSuccessWhenAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            session.CurrentCompanyRef.Returns(1);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo);
            var model = fixture.Create<AssetAddViewModel>();
            var result = await assetModule.AddAssetAsync(model);
            result.Status.Should().Be(RequestStatus.Success);
        }

        /// <summary>
        /// The AddAssestShouldThrowWhenSessionHasNoCompany
        /// </summary>
        [Fact]
        public void AddAssestShouldThrowWhenSessionHasNoCompany()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo);
            var model = fixture.Create<AssetAddViewModel>();
            Func<Task> act = async () => await assetModule.AddAssetAsync(model);
            act.Should().Throw<ArgumentException>();
        }
    }
}
