using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {

        Task<string> SaveBlobAsync(string containerName, IFormFile file);

        Task<string> RemoveBlobAsync(string containerName, string blobName);

        string GetProtectedUrl(string container, string blob, DateTime expireDate);

    }

    public class AzureBlobStorageService : IStorageService
    {
        private readonly AzureStorageOptions _options;
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(AzureStorageOptions options )
        {
            _options = options;
            _blobServiceClient = new BlobServiceClient(options.StorageConnectionString);
        }

        public string GetProtectedUrl(string container, string blob, DateTime expireDate)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveBlobAsync(string containerName, string blobName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveBlobAsync(string containerName, IFormFile file)
        {
            string fileName = file.Name;
            string extenstion = Path.GetExtension(fileName);

            string newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid()}-{extenstion}";

            using (var stream = file.OpenReadStream())
            {
                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                await container.CreateIfNotExistsAsync();
                var blob = container.GetBlobClient(newFileName);
                await blob.UploadAsync(stream);
                return $"{_options.AccountUrl}/{containerName}/{newFileName}";
            }

        }
    }
}
