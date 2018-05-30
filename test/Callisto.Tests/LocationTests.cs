using AutoFixture;
using Callisto.Module.Locations;
using Callisto.Module.Locations.Interfaces;
using Callisto.Module.Locations.Repository.Models;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="LocationTests" />
    /// </summary>
    public class LocationTests
    {
        /// <summary>
        /// The GetLocationShouldSucceedWhenAllIsWell
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetLocationShouldSucceedWhenAllIsWell()
        {
            var fixture = new Fixture();
            var session = Substitute.For<ICallistoSession>();
            var repo = Substitute.For<ILocationRepository>();
            repo.GetLocationById(Arg.Any<long>()).Returns(c => fixture.Create<Location>());
            var location = new LocationModule(session, Substitute.For<ILogger<LocationModule>>(), repo);

            var result = await location.GetLocation(Arg.Any<long>());

            result.Status.Should().Be(RequestStatus.Success);
        }

        /// <summary>
        /// The GetLocationWhenItDoesNotExistsReturnsWarning
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetLocationWhenItDoesNotExistsReturnsWarning()
        {
            var fixture = new Fixture();
            var session = Substitute.For<ICallistoSession>();
            var repo = Substitute.For<ILocationRepository>();
            var location = new LocationModule(session, Substitute.For<ILogger<LocationModule>>(), repo);
            var result = await location.GetLocation(Arg.Any<long>());
            result.Status.Should().Be(RequestStatus.Warning);
        }

        /// <summary>
        /// The UpsertLocationCreatesNewLocationWhenNotExists
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpsertLocationCreatesNewLocationWhenNotExists()
        {
            var fixture = new Fixture();
            var session = Substitute.For<ICallistoSession>();
            var repo = Substitute.For<ILocationRepository>();
            var location = new LocationModule(session, Substitute.For<ILogger<LocationModule>>(), repo);
            var result = await location.UpsertLocation(fixture.Create<LocationViewModel>());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).AddLocation(Arg.Any<Location>());
        }

        /// <summary>
        /// The UpsertLocationUpdatesLocationWhenExists
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpsertLocationUpdatesLocationWhenExists()
        {
            var fixture = new Fixture();
            var session = Substitute.For<ICallistoSession>();
            var repo = Substitute.For<ILocationRepository>();
            repo.GetLocationByPlaceId(Arg.Any<string>(), Arg.Any<long>()).Returns(c => fixture.Create<Location>());
            var location = new LocationModule(session, Substitute.For<ILogger<LocationModule>>(), repo);
            var result = await location.UpsertLocation(fixture.Create<LocationViewModel>());
            result.Status.Should().Be(RequestStatus.Success);
            repo.Received(1).SaveLocation(Arg.Any<Location>());
        }
    }
}
