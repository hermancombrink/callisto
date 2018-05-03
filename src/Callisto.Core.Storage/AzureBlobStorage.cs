using Callisto.Core.Storage.Options;
using Callisto.SharedModels.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace Callisto.Core.Storage
{
    /// <summary>
    /// Defines the <see cref="AzureBlobStorage" />
    /// </summary>
    public class AzureBlobStorage : IStorage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBlobStorage"/> class.
        /// </summary>
        /// <param name="options">The <see cref="IOptions{StorageOptions}"/></param>
        /// <param name="logger">The <see cref="ILogger{AzureBlobStorage}"/></param>
        public AzureBlobStorage(IOptions<StorageOptions> options, ILogger<AzureBlobStorage> logger)
        {
            Options = options.Value;
            Logger = logger;
        }

        /// <summary>
        /// Gets the Options
        /// </summary>
        private StorageOptions Options { get; }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<AzureBlobStorage> Logger { get; }

        /// <summary>
        /// Gets the BlobClient
        /// </summary>
        public CloudBlobClient BlobClient => GetStorageAccount().CreateCloudBlobClient();

        /// <summary>
        /// The GetStorageAccount
        /// </summary>
        /// <returns>The <see cref="CloudStorageAccount"/></returns>
        public CloudStorageAccount GetStorageAccount()
        {
            if (!CloudStorageAccount.TryParse(Options.ConnectionString, out CloudStorageAccount account))
            {
                Logger.LogError($"Failed to obtain storage account");
            }
            else
            {
                Logger.LogInformation("Obtained storage account");
            }

            return account ?? throw new InvalidOperationException("Account not available");
        }

        /// <summary>
        /// The GetBlobContainer
        /// </summary>
        /// <param name="reference">The <see cref="string"/></param>
        /// <returns>The <see cref="CloudBlobContainer"/></returns>
        public async Task<CloudBlobContainer> GetBlobContainer(string reference)
        {
            var container = BlobClient.GetContainerReference(reference);
            await container.CreateIfNotExistsAsync();

            var containerPermissions = new BlobContainerPermissions();

            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Container;

            await container.SetPermissionsAsync(containerPermissions);

            return container;
        }

        /// <summary>
        /// The GetFile
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<Uri> GetFileAsync(string fileName, string reference)
        {
            var container = await GetBlobContainer(reference);
            var blob = container.GetBlockBlobReference(fileName);
            return blob.Uri;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="fileName">The <see cref="string"/></param>
        /// <param name="reference">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<bool> DeleteAsync(string fileName, string reference)
        {
            var container = await GetBlobContainer(reference);
            var blob = container.GetBlockBlobReference(fileName);
            return await blob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// The SaveFile
        /// </summary>
        /// <param name="file">The <see cref="IFormFile"/></param>
        /// <param name="relativePath">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<string> SaveFile(byte[] fileData, string fileName, string reference)
        {
            var container = await GetBlobContainer(reference);

            var blob = container.GetBlockBlobReference(fileName);

            await blob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
            Logger.LogInformation($"File {fileName} uploaded");

            return blob.Uri.AbsoluteUri;
        }
    }
}
