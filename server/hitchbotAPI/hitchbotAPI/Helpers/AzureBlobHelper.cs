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
        public const string JS_LOCATION_FILE_NAME = "testLocations";
        public const string JS_FILE_EXTENSION = ".js";
        private const string JS_CONTAINER_NAME = "hbjs";

        public static string UploadLocationJsAndGetPublicUrl(string localRootFileDirectory, string fileName, int ID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imgContainer = blobClient.GetContainerReference(JS_CONTAINER_NAME);

            CloudBlockBlob newBlob = imgContainer.GetBlockBlobReference(JS_LOCATION_FILE_NAME + ID + JS_FILE_EXTENSION);

            newBlob.DeleteIfExists();

            using (var fileString = System.IO.File.OpenRead(localRootFileDirectory + fileName))
            {
                newBlob.UploadFromStream(fileString);
                newBlob.Properties.ContentType = "text/javascript";

                //we don't want the browser to cache this file because it contains data which is very likely to change frequently.
                newBlob.Properties.CacheControl = "public, max-age=0";
                newBlob.SetProperties();
            }

            return newBlob.Uri.ToString();
        }

        private static TimeSpan JS_REBUILD_INTERVAL = new TimeSpan(1, 0, 0);

        /// <summary>
        /// Determines if the javascript file containing the map data should be rebuilt based on the time passed since it was last built.
        /// </summary>
        /// <param name="fileName">the filename of the javascript file that may need to be rebuilt.</param>
        /// <param name="ID">The ID if the javascript file (also the hitcbotID) to determine if it needs to be rebuilt</param>
        /// <returns></returns>
        public static bool ShouldJsBeRebuilt(string fileName, int ID)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer imgContainer = blobClient.GetContainerReference(JS_CONTAINER_NAME);

            CloudBlockBlob blob = imgContainer.GetBlockBlobReference(JS_LOCATION_FILE_NAME + ID + JS_FILE_EXTENSION);

            if (blob.Exists())
            {
                DateTimeOffset LastModified = blob.Properties.LastModified ?? new DateTime();

                return DateTimeOffset.UtcNow - LastModified > JS_REBUILD_INTERVAL;
            }
            return true;
        }

        public static void UploadJStoAzure(int hitchbotID)
        {
            string TargetLocation = Helpers.PathHelper.GetJsBuildPath();
            //this is a mess!
            Helpers.AzureBlobHelper.UploadLocationJsAndGetPublicUrl(TargetLocation, Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + hitchbotID + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, hitchbotID);
        }
    }
}