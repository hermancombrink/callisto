using AutoFixture;
using Callisto.Module.Assets;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Storage;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Linq;
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
        /// Gets the Storage
        /// </summary>
        public IStorage Storage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsTests"/> class.
        /// </summary>
        public AssetsTests()
        {
            Storage = Substitute.For<IStorage>();
        }

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
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetAddViewModel>();
            model.ParentId = Guid.Empty;
            var result = await assetModule.AddAssetAsync(model);
            result.Status.Should().Be(RequestStatus.Success);
        }

        /// <summary>
        /// The AddAssetShouldUseParentWithIdSpecified
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddAssetShouldUseParentWithIdSpecified()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            session.CurrentCompanyRef.Returns(1);
            repo.GetAssetById(Arg.Any<Guid>()).Returns(fixture.Create<Asset>());
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetAddViewModel>();
            var result = await assetModule.AddAssetAsync(model);
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }

        /// <summary>
        /// The AddAssetShouldUseParentWithIdSpecifiedAndNotFound
        /// </summary>
        [Fact]
        public void AddAssetShouldUseParentWithIdSpecifiedAndNotFound()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            session.CurrentCompanyRef.Returns(1);
            repo.GetAssetById(Arg.Any<Guid>()).Returns<Asset>(c => null);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetAddViewModel>();
            Func<Task> act = async () => await assetModule.AddAssetAsync(model);
            act.Should().Throw<InvalidOperationException>();
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
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
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetAddViewModel>();
            Func<Task> act = async () => await assetModule.AddAssetAsync(model);
            act.Should().Throw<ArgumentException>();
        }

        /// <summary>
        /// The GetAssetByShouldReturnViewModel
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAssetByShouldReturnViewModel()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            var asset = fixture.Create<Asset>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(asset);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetAddViewModel>();
            var result = await assetModule.GetAssetAsync(asset.Id);
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }

        /// <summary>
        /// The GetAssetByShouldReturnViewModel
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task SaveAssetShouldReturnSuccessWithAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            var viewModel = fixture.Create<AssetViewModel>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(fixture.Create<Asset>());
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetViewModel>();
            var result = await assetModule.SaveAssetAsync(viewModel);
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
        }

        /// <summary>
        /// The SaveAssetShouldThrowWhenAssetNotFound
        /// </summary>
        [Fact]
        public void SaveAssetShouldThrowWhenAssetNotFound()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            session.CurrentCompanyRef.Returns(1);
            repo.GetAssetById(Arg.Any<Guid>()).Returns<Asset>(c => null);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetViewModel>();
            Func<Task> act = async () => await assetModule.SaveAssetAsync(model);
            act.Should().Throw<InvalidOperationException>();
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }

        /// <summary>
        /// The SaveAssetShouldThrowWhenAssetNotFound
        /// </summary>
        [Fact]
        public async Task GetAssetTreeShouldReturnWithAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            repo.GetAssetTree(Arg.Any<long>(), null).Returns(fixture.Build<AssetTreeModel>().CreateMany(10));

            session.CurrentCompanyRef.Returns(1);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.GetAssetTreeAsync();
            result.Status.Should().Be(RequestStatus.Success);
            result.Result.Count().Should().Be(10);
            repo.Received(1).GetAssetTree(Arg.Any<long>(), null);
        }

        /// <summary>
        /// The GetAssetTreeWithParentShouldReturnWithAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAssetTreeWithParentShouldReturnWithAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();

            repo.GetAssetTree(Arg.Any<long>(), Arg.Any<long>()).Returns(fixture.Build<AssetTreeModel>().CreateMany(10));
            var parent = fixture.Create<Asset>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(parent);
            session.CurrentCompanyRef.Returns(1);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.GetAssetTreeAsync(parent.Id);
            result.Status.Should().Be(RequestStatus.Success);
            result.Result.Count().Should().Be(10);
            repo.Received(1).GetAssetTree(Arg.Any<long>(), Arg.Any<long>());
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }

        /// <summary>
        /// The GetAssetTreeWithParentShouldThrowWhenParentNotFound
        /// </summary>
        [Fact]
        public void GetAssetTreeWithParentShouldThrowWhenParentNotFound()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();

            repo.GetAssetTree(Arg.Any<long>(), Arg.Any<long>()).Returns(fixture.Build<AssetTreeModel>().CreateMany(10));
            var parent = fixture.Create<Asset>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns<Asset>(c => null);
            session.CurrentCompanyRef.Returns(1);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            Func<Task> act = async () => await assetModule.GetAssetTreeAsync(parent.Id);
            act.Should().Throw<InvalidOperationException>();
            repo.Received(0).GetAssetTree(Arg.Any<long>(), Arg.Any<long>());
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }
    }
}
