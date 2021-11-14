using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Services
{
    public class BlobsService : IBlobsService
    {
        private readonly IConfiguration _configuration;

        public BlobsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<BlobContainerClient> GetContainer()
        {
            //Create a unique name for the container
            string containerName = _configuration.GetSection("BlobDetail")["employeeimgblobcontainer"];
            string connectionString = _configuration.GetSection("BlobDetail")["AZURE_STORAGE_CONNECTION_STRING"];
            try
            {
                //create a BlobContainerClient 
                BlobContainerClient blobContainer = new(connectionString, containerName);

                //or use this code to create the container directly, if it does not exist.
                await blobContainer.CreateIfNotExistsAsync();

                return blobContainer;
            }
            catch (Exception exception)
            {
                var ex = exception;
                throw;
            }
        }

        private async Task<BlobClient> PrepareBlobClient(string fileName)
        {
            BlobContainerClient containerClient = await GetContainer();
            try
            {
                // Get a reference to a blob
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                return blobClient;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFile(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;

                BlobClient blobClient = await PrepareBlobClient(fileName);
                try
                {
                    // Upload data from the local file
                    await blobClient.UploadAsync(formFile.OpenReadStream(), true);
                }
                catch (Exception)
                {
                    throw;
                }

                return blobClient.Name;
            }
            return "";
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            BlobContainerClient containerClient = await GetContainer();
            Azure.Response<bool> response = await containerClient.DeleteBlobIfExistsAsync(fileName);
            return response.Value;
        }
    }
}
