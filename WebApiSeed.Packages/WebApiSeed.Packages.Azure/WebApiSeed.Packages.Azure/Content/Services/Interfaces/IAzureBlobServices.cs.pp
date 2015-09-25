namespace $rootnamespace$.Services.Interfaces
{
    using Dtos;
    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IAzureBlobServices
    {
        CloudBlobContainer GetWebApiContainer(string containerKey);

        void DeleteFile(CloudBlobContainer blobHelper, string filename);

        AzureFileDetailsDto UploadFile(CloudBlobContainer blobHelper, string filename, string filepath,
            string filetype);

        AzureFileDetailsDto UploadFileFromByteArray(CloudBlobContainer blobHelper, string filename, byte[] byteArray,
            string filetype);
    }
}