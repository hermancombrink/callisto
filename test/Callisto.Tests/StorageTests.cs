using Callisto.Core.Storage;
using Callisto.Core.Storage.Options;
using Callisto.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Callisto.Tests
{
    /// <summary>
    /// Defines the <see cref="StorageTests" />
    /// </summary>
    public class StorageTests 
    {
        public StorageTests()
        {

        }
        /// <summary>
        /// Defines the options
        /// </summary>
        public OptionsWrapper<StorageOptions> Options = new OptionsWrapper<StorageOptions>(new StorageOptions()
        {
            ConnectionString = "UseDevelopmentStorage=true"
        });

        /// <summary>
        /// The StorageShouldBeAbleToObtainStorageAccount
        /// </summary>
        [Fact(Skip = "Requires emulator install")]
        public void StorageShouldBeAbleToObtainStorageAccount()
        {
            var logger = Substitute.For<ILogger<AzureBlobStorage>>();

            var storage = new AzureBlobStorage(Options, logger);

            var account = storage.GetStorageAccount();
            account.Should().NotBeNull();
        }

        /// <summary>
        /// The StorageShouldFailWhenInvalidConnectionIsUsed
        /// </summary>
        [Fact(Skip = "Requires emulator install")]
        public void StorageShouldFailWhenInvalidConnectionIsUsed()
        {
            var logger = Substitute.For<ILogger<AzureBlobStorage>>();

            var storage = new AzureBlobStorage(new OptionsWrapper<StorageOptions>(new StorageOptions()
            {
                ConnectionString = "Test=true"
            }), logger);

            Action act = () => storage.GetStorageAccount();
            act.Should().Throw<InvalidOperationException>();
        }

        /// <summary>
        /// The StorageShouldObtainBlobReference
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact(Skip = "Requires emulator install")]
        public async Task StorageShouldObtainBlobReference()
        {
            var logger = Substitute.For<ILogger<AzureBlobStorage>>();

            var storage = new AzureBlobStorage(Options, logger);

            var blob = await storage.GetBlobContainer("test");

            blob.Should().NotBeNull();

            await blob.DeleteAsync();
        }

        /// <summary>
        /// The StorageShouldSaveFile
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact(Skip = "Requires emulator install")]
        public async Task StorageShouldSaveFile()
        {
            var logger = Substitute.For<ILogger<AzureBlobStorage>>();

            var storage = new AzureBlobStorage(Options, logger);

            var image = Substitute.For<IFormFile>();
            image.OpenReadStream().Returns(c => File.OpenRead("TestData\\test.png"));
            image.FileName.Returns(c => "TestFile.png");
            var fileData = File.ReadAllBytes("TestData\\test.png");

            var uri = await storage.SaveFile(fileData, "test.png", "images");

            var web = new HttpClient();

            var response = await web.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var result = await storage.DeleteAsync("test.png", "images");

            result.Should().Be(true);
        }
    }
}
