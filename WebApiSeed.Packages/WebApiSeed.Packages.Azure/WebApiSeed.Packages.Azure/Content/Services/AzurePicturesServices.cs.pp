namespace $rootnamespace$.Services
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net.Http;
    using Dtos;
    using Interfaces;
    using Microsoft.Azure;
    using Resources;

    public class AzurePicturesServices : IAzurePicturesServices
    {
        private readonly IAzureBlobServices _azureBlobServices;

        private readonly string _userPicturesBlobContainer =
            CloudConfigurationManager.GetSetting("UserPicturesBlobContainer");

        public AzurePicturesServices(IAzureBlobServices azureBlobServices)
        {
            _azureBlobServices = azureBlobServices;
        }

        /// <summary>
        ///     Upload user picture to azure
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <param name="userPicture">Picture file</param>
        /// <returns>Azure file details</returns>
        /// <exception cref="BadImageFormatException"></exception>
        public AzureFileDetailsDto SavePicture(string fileName, MultipartFileData userPicture)
        {
            string imageType;

            var fileStream = new FileStream(userPicture.LocalFileName, FileMode.Open);
            var image = Image.FromStream(fileStream);
            fileStream.Close();

            if (image.RawFormat.Equals(ImageFormat.Jpeg))
                imageType = "image/jpeg";
            else if (image.RawFormat.Equals(ImageFormat.Png))
                imageType = "image/png";
            else if (image.RawFormat.Equals(ImageFormat.Gif))
                imageType = "image/gif";
            else
                throw new BadImageFormatException(ExceptionErrorMessages.BadImageFormatException);

            if (String.IsNullOrEmpty(imageType))
                return null;

            var blobHelper = _azureBlobServices.GetWebApiContainer(_userPicturesBlobContainer);

            _azureBlobServices.DeleteFile(blobHelper, fileName);

            fileName = Guid.NewGuid().ToString("N");

            var uploadedPhoto = _azureBlobServices.UploadFile(blobHelper, fileName, userPicture.LocalFileName, imageType);

            return uploadedPhoto;
        }

        public AzureFileDetailsDto SavePictureFromByteArray(string fileName, byte[] userPicture)
        {
            string imageType;

            var image = Image.FromStream(new MemoryStream(userPicture));

            if (image.RawFormat.Equals(ImageFormat.Jpeg))
                imageType = "image/jpeg";
            else if (image.RawFormat.Equals(ImageFormat.Png))
                imageType = "image/png";
            else if (image.RawFormat.Equals(ImageFormat.Gif))
                imageType = "image/gif";
            else
                throw new BadImageFormatException(ExceptionErrorMessages.BadImageFormatException);

            if (String.IsNullOrEmpty(imageType))
                return null;

            var blobHelper = _azureBlobServices.GetWebApiContainer(_userPicturesBlobContainer);

            if (!String.IsNullOrEmpty(fileName))
                _azureBlobServices.DeleteFile(blobHelper, fileName);

            fileName = Guid.NewGuid().ToString("N");

            var uploadedPhoto = _azureBlobServices.UploadFileFromByteArray(blobHelper, fileName, userPicture, imageType);

            return uploadedPhoto;
        }

        public void DeletePicture(string filename)
        {
            var blobHelper = _azureBlobServices.GetWebApiContainer(_userPicturesBlobContainer);

            _azureBlobServices.DeleteFile(blobHelper, filename);
        }
    }
}