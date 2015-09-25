namespace $rootnamespace$.Services.Interfaces
{
    using System.Net.Http;
    using Dtos;

    public interface IAzurePicturesServices
    {
        AzureFileDetailsDto SavePicture(string fileName, MultipartFileData userPicture);

        AzureFileDetailsDto SavePictureFromByteArray(string fileName, byte[] userPicture);

        void DeletePicture(string filename);
    }
}