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
        public const string JS_LOCATION_FILE_NAME = "testLocations.js";
        private const string JS_CONTAINER_NAME = "hbjs";

        public static string UploadLocationJsAndGetPublicUrl(string localRootFileDirectory, string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imgContainer = blobClient.GetContainerReference(JS_CONTAINER_NAME);

            CloudBlockBlob newBlob = imgContainer.GetBlockBlobReference(JS_LOCATION_FILE_NAME);

            newBlob.DeleteIfExists();

            using (var fileString = System.IO.File.OpenRead(localRootFileDirectory + fileName))
            {
                newBlob.UploadFromStream(fileString);
                newBlob.Properties.ContentType = "text/javascript";
                newBlob.Properties.CacheControl = "public, max-age=0";
                newBlob.SetProperties();
            }

            return newBlob.Uri.ToString();
        }
    }
}