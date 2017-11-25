using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace News.Helpers
{
    public class SenTimeBlobContainer
    {
        #region Constants
        private const string StorageConnectionString = "AzureStorageConnectionString";
        private const string SenTimeBlobFolder = "sentimefiles";
        private const string ProjectBlobFolder = "monita-project";
        private const string OrganisationBlobFolder = "monita-organisation";
        public const string OriginalPicture = "picture_original";
        public const string ProjectLogo = "project_logo_";
        public const string ProjectLogoThumbnail = "project_logo_thumbnail_";
        public const string ProfileAvatarOriginal = "avatar_original_";
        public const string ProfileAvatarBig = "avatar_big_";
        public const string ProfileAvatarSmall = "avatar_small_";
        public const string OrganisationLogoLarge = "organisation_logo_large_";
        public const string OrganisationLogoMedium = "organisation_logo_medium_";
        public const string OrganisationLogoSmall = "organisation_logo_small_";
        public const string Jpeg = "Jpeg";
        public const string Png = "Png";
        private const int ThreeHundred = 300;
        private const int TwoHundred = 200;
        private const int OneHundred = 100;
        private const int Fifty = 50;
        private const int StartIndex = 0;
        #endregion

        private CloudBlobContainer _cloudBlobContainer;

        public SenTimeBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings[StorageConnectionString]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();


            CloudBlobContainer container = blobClient.GetContainerReference(SenTimeBlobFolder);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            _cloudBlobContainer = container;
        }

        #region Execution Methods

        /// <summary>
        /// Method saves image to blob in azure and returns saved image url.
        /// </summary>
        /// <param name="fileContent">Uploaded image in string content.</param>
        /// <param name="oldPicture">Old picture url for delete</param>
        /// <param name="picturePreName">Picture Pre Name</param>
        /// <returns>Project logo file url.</returns>
        public string GetOriginalPictureAndSave(string fileContent, string oldPicture, string picturePreName)
        {
            DeleteIfExistFile(oldPicture);

            var pictureUrl = SaveAndGetFileName(fileContent, Jpeg, picturePreName);

            return pictureUrl;
        }

        /// <summary>
        /// Method saves image to blob in azure and returns saved image url.
        /// </summary>
        /// <param name="fileContent">Uploaded image in string content.</param>
        /// <param name="oldThumbnail">Old image url for delete</param>
        /// <returns>Project thumbnail file url.</returns>
        public string GetProjectThumbnailAndSave(string fileContent, string oldThumbnail)
        {
            DeleteIfExistFile(oldThumbnail);

            var fileName = SaveAndGetResizedImage(fileContent, ThreeHundred, TwoHundred, Png, ProjectLogoThumbnail);

            return fileName;
        }

        /// <summary>
        /// Method saves image to blob in azure and returns saved image url.
        /// </summary>
        /// <param name="fileContent">Uploaded image in string content.</param>
        /// /// <param name="oldAvatarBig">Old image url for delete</param>
        /// <returns>Profile big(200*100) avatar file url.</returns>
        public string GetProfileBigAvatar(string fileContent, string oldAvatarBig)
        {
            DeleteIfExistFile(oldAvatarBig);

            var fileName = SaveAndGetResizedImage(fileContent, TwoHundred, OneHundred, Png, ProfileAvatarBig);

            return fileName;
        }

        /// <summary>
        /// Method saves image to blob in azure and returns saved image url.
        /// </summary>
        /// <param name="fileContent">Uploaded image in string content.</param>
        /// <param name="oldAvatarSmall">Old image url for delete</param>
        /// <returns>Profile samll(50*50) avatar file url.</returns>
        public string GetProfileSmallAvatar(string fileContent, string oldAvatarSmall)
        {
            DeleteIfExistFile(oldAvatarSmall);

            var fileName = SaveAndGetResizedImage(fileContent, Fifty, Fifty, Png, ProfileAvatarSmall);

            return fileName;
        }


        /// <summary>
        /// Method saves image to blob in azure and returns saved image url.
        /// </summary>
        /// <param name="fileContent">Uploaded image in string content.</param>
        /// <param name="oldPicture">Old picture url for delete</param>
        /// <param name="width">Width of picture</param>
        /// <param name="height">Height of picture</param>
        /// <param name="fileExtention">Picture extention</param>
        /// <param name="picturePreName">Picture Pre Name</param>
        /// <returns>Picture file url.</returns>
        public string GetPictureWithDefinedSizeAndExtention(string fileContent, string oldPicture, int width, int height, string fileExtention, string picturePreName)
        {
            DeleteIfExistFile(oldPicture);

            var fileName = SaveAndGetResizedImage(fileContent, width, height, fileExtention, picturePreName);

            return fileName;
        }

        #endregion
        #region Private Helpers Methods

        /// <summary>
        /// Deletes old file if it exist.
        /// </summary>
        /// <param name="oldFile">Old file name.</param>
        private void DeleteIfExistFile(string oldFile)
        {
            if (oldFile != null)
            {
                CloudBlockBlob oldBlob = _cloudBlobContainer.GetBlockBlobReference(oldFile);
                oldBlob.DeleteIfExistsAsync();
            }
        }

        /// <summary>
        /// Save file and returns it's url(file name).
        /// </summary>
        /// <param name="fileContent">Uploaded file in string content.</param>
        /// <param name="fileType">File type.</param>
        /// <param name="partOfName">File part name.</param>
        /// <returns>File name.</returns>
        private string SaveAndGetFileName(string fileContent, string fileType, string partOfName)
        {
            byte[] fileBytes = Convert.FromBase64String(fileContent);
            var fileUrl = partOfName + Guid.NewGuid() + Path.GetExtension(fileContent);

            SaveFile(fileUrl, fileType, fileBytes);

            return fileUrl;
        }

        /// <summary>
        /// Save file, resize it and returns it's url(file name)
        /// </summary>
        /// <param name="imageContent">Uploaded image file in string content.</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="fileType">Image type.</param>
        /// <param name="partOfName">Part of image file name.</param>
        /// <returns>Image file name.</returns>
        private string SaveAndGetResizedImage(string imageContent, int width, int height, string fileType, string partOfName)
        {
            var webImage = CreateResizeImage(imageContent, width, height, partOfName);
            var fileBytes = webImage.GetBytes();

            SaveFile(webImage.FileName, fileType, fileBytes);

            return webImage.FileName;
        }

        /// <summary>
        /// Create WebImage object and returns it.
        /// </summary>
        /// <param name="imageContent">Uploaded image file in string content.</param>
        /// <param name="width">Image width.</param>
        /// <param name="height">Image height.</param>
        /// <param name="partOfName">Part of image file name.</param>
        /// <returns>WebImage object.</returns>
        private WebImage CreateResizeImage(string imageContent, int width, int height, string partOfName)
        {
            byte[] imageBytes = Convert.FromBase64String(imageContent);

            var webImage = new WebImage(imageBytes);
            webImage.Resize(width, height, false, false);
            webImage.FileName = partOfName + Guid.NewGuid() + Path.GetExtension(imageContent);

            return webImage;
        }

        /// <summary>
        /// Save file in azure blob storage.
        /// </summary>
        /// <param name="fileName">File name(url).</param>
        /// <param name="fileType">File type.</param>
        /// <param name="fileContent">File content in bytes array.</param>
        public void SaveFile(string fileName, string fileType, byte[] fileContent)
        {
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = fileType;
            blob.UploadFromByteArray(fileContent, StartIndex, fileContent.Length);
        }


        #endregion
    }
}