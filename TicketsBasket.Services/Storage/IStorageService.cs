using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {

        Task<string> SaveBlobAsync(string containerName, IFormFile file, BlobType blobType);

        Task RemoveBlobAsync(string containerName, string blobName);

        string GetProtectedUrl(string containerName, string blob, DateTimeOffset expireDate);

    }

    public enum BlobType
    {
        Image,
        Document
    }
}
