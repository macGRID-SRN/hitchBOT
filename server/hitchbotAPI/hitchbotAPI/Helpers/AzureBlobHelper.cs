using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace hitchbotAPI.Helpers
{
    public static class AzureBlobHelper
    {
        public static string UploadImageAndGetPublicUrl(string localRootFileDirectory, string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imgContainer = blobClient.GetContainerReference("img");

            CloudBlockBlob newBlob = imgContainer.GetBlockBlobReference(DateTime.UtcNow.ToString() + " - " + fileName);

            using (var fileString = System.IO.File.OpenRead(localRootFileDirectory + fileName))
            {
                newBlob.UploadFromStream(fileString);
                newBlob.Properties.ContentType = "image/webp";
                newBlob.SetProperties();
            }

            return newBlob.Uri.ToString();
        }
    }
}