namespace $rootnamespace$.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Dtos;
    using Interfaces;
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class AzureBlobServices : IAzureBlobServices
    {
        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageConnectionString"));

        public CloudBlobContainer GetWebApiContainer(string containerKey)
        {
            // Create the blob client 
            var blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container 
            // Container name must use lower case
            var container = blobClient.GetContainerReference(containerKey);

            // Create the container if it doesn't already exist
            container.CreateIfNotExists();

            // Enable public access to blob
            var permissions = container.GetPermissions();
            if (permissions.PublicAccess != BlobContainerPublicAccessType.Off) return container;

            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            container.SetPermissions(permissions);

            return container;
        }

        public AzureFileDetailsDto UploadFile(CloudBlobContainer blobHelper, string filename, string filepath,
            string filetype)
        {
            var blob = blobHelper.GetBlockBlobReference(filename);

            blob.Properties.ContentType = filetype;

            blob.UploadFromFile(filepath, FileMode.OpenOrCreate);

            File.Delete(filepath);

            var uploadedFile = new AzureFileDetailsDto
            {
                ContentType = blob.Properties.ContentType,
                Name = blob.Name,
                Size = blob.Properties.Length,
                Location = blob.Uri.AbsoluteUri
            };

            return uploadedFile;
        }

        public AzureFileDetailsDto UploadFileFromByteArray(CloudBlobContainer blobHelper, string filename,
            byte[] byteArray, string filetype)
        {
            var blob = blobHelper.GetBlockBlobReference(filename);

            blob.Properties.ContentType = filetype;

            using (var stream = new MemoryStream(byteArray, false))
            {
                blob.UploadFromStream(stream);
            }

            var uploadedFile = new AzureFileDetailsDto
            {
                ContentType = blob.Properties.ContentType,
                Name = blob.Name,
                Size = blob.Properties.Length,
                Location = blob.Uri.AbsoluteUri
            };

            return uploadedFile;
        }

        public void DeleteFile(CloudBlobContainer blobHelper, string filename)
        {
            var blob = blobHelper.GetBlockBlobReference(filename);

            try
            {
                blob.Delete();
            }
            catch (StorageException e)
            {
                Trace.TraceError(String.Format("[AzureBlobServices] {0} not found in blob container", filename));
            }
        }
    }
}