using AutoFixture;
using Callisto.Module.Assets;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Storage;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System;
using System.IO;
using System.Linq;
using System.Text;
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
        public async Task GetAssetByIdShouldReturnWhenAllIswell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            var asset = fixture.Create<Asset>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(asset);
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.GetAssetAsync(asset.Id);
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
        }

        /// <summary>
        /// The GetAssetByShouldReturnViewModel
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAssetDetailByIdShouldReturnWhenAllIswell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var location = Substitute.For<ILocationModule>();
            var session = Substitute.For<ICallistoSession>();

            location.GetLocation(Arg.Any<long>()).Returns(c => fixture.Create<RequestResult<LocationViewModel>>());
            session.Location.Returns(c => location);
            var asset = fixture.Create<Asset>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(asset);
            repo.GetAssetLocationByAssetId(Arg.Any<long>()).Returns(c => fixture.Create<AssetLocation>());
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.GetAssetDetailsAsync(asset.Id);

            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
            location.Received(1).GetLocation(Arg.Any<long>());
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
            var location = Substitute.For<ILocationModule>();
            var session = Substitute.For<ICallistoSession>();
            location.UpsertLocation(Arg.Any<LocationViewModel>()).Returns(c => RequestResult<long>.Success(1));
            session.Location.Returns(c => location);
            var viewModel = fixture.Create<AssetDetailViewModel>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(fixture.Create<Asset>());
            repo.BeginTransaction().Returns(c => Substitute.For<IDbContextTransaction>());

            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetViewModel>();
            var result = await assetModule.SaveAssetAsync(viewModel);
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(2).GetAssetById(Arg.Any<Guid>());
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
            repo.Received(1).AddAssetLocation(Arg.Any<AssetLocation>());
        }

        [Fact]
        public void SaveAssetShouldFailWhenLocationFails()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var location = Substitute.For<ILocationModule>();
            var session = Substitute.For<ICallistoSession>();
            location.UpsertLocation(Arg.Any<LocationViewModel>()).Returns(c => RequestResult<long>.Failed("oops"));
            session.Location.Returns(c => location);
            var viewModel = fixture.Create<AssetDetailViewModel>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(fixture.Create<Asset>());
            repo.BeginTransaction().Returns(c => Substitute.For<IDbContextTransaction>());

            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var model = fixture.Create<AssetViewModel>();
            Func<Task> act = async () => { var result = await assetModule.SaveAssetAsync(viewModel); };
            act.Should().Throw<InvalidOperationException>();
            repo.Received(2).GetAssetById(Arg.Any<Guid>());
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
            repo.Received(0).AddAssetLocation(Arg.Any<AssetLocation>());
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
            var model = fixture.Create<AssetDetailViewModel>();
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

        [Fact]
        public async Task UploadAssetPicShouldSucceedWhenAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var auth = Substitute.For<IAuthenticationModule>();
            var session = Substitute.For<ICallistoSession>();
            var storage = Substitute.For<IStorage>();
            var file = Substitute.For<IFormFile>();
            file.OpenReadStream().Returns(c => new MemoryStream(Encoding.UTF8.GetBytes("abc")));
            storage.SaveFile(Arg.Any<byte[]>(), Arg.Any<string>(), Arg.Any<string>()).Returns(c => "https://test.com");
            session.Authentication.Returns(c => auth);
            auth.GetCompanyByRefId(Arg.Any<long>()).Returns(c => RequestResult<CompanyViewModel>.Success(fixture.Create<CompanyViewModel>()));

            repo.GetAssetById(Arg.Any<Guid>()).Returns(fixture.Create<Asset>());
            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, storage);
            
            var result = await assetModule.UploadAssetPicAsync(file, Guid.NewGuid());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).GetAssetById(Arg.Any<Guid>());
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
            result.Result.Should().Be("https://test.com");
        }


        [Fact]
        public async Task UpdateParentShouldSucceedWhenAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(c => fixture.Create<Asset>());

            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.UpdateParentAsync(Guid.NewGuid(), Guid.NewGuid());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
        }

        [Fact]
        public async Task UpdateParentShouldSucceedWhenParentIsNull()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(c => fixture.Create<Asset>());

            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.UpdateParentAsync(Guid.NewGuid());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).SaveAssetAsync(Arg.Any<Asset>());
        }

        [Fact]
        public async Task RemoveAssetShouldSucceedWhenAllIsWell()
        {
            var fixture = new Fixture();
            var repo = Substitute.For<IAssetsRepository>();
            var session = Substitute.For<ICallistoSession>();
            repo.GetAssetById(Arg.Any<Guid>()).Returns(c => fixture.Create<Asset>());

            var assetModule = new AssetsModule(session, Substitute.For<ILogger<AssetsModule>>(), repo, Storage);
            var result = await assetModule.RemoveAssetAsync(Guid.NewGuid());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).RemoveAssetAsync(Arg.Any<Asset>());
        }
    }
}
