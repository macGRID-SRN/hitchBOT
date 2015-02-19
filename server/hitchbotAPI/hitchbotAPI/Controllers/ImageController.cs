using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using LinqToTwitter;
using System.Threading.Tasks;

namespace hitchbotAPI.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        public bool AddImage(int HitchBotID, int locationID, string timeTaken, string URL)
        {
            DateTime TimeTaken = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchBOT = db.hitchBOTs.First(l => l.ID == HitchBotID);
                var location = db.Locations.First(l => l.ID == locationID);

                var image = new Models.Image()
                {
                    Location = location,
                    HitchBOT = hitchBOT,
                    url = URL,
                    TimeAdded = DateTime.UtcNow,
                    TimeTaken = TimeTaken
                };

                db.Images.Add(image);
                db.SaveChanges();
            }

            return true;
        }

        [HttpPost]
        public bool AddImage(int HitchBotID, string timeTaken, string URL)
        {
            DateTime TimeTaken = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchBOT = db.hitchBOTs.First(l => l.ID == HitchBotID);

                var image = new Models.Image()
                {
                    HitchBOT = hitchBOT,
                    url = URL,
                    TimeAdded = DateTime.UtcNow,
                    TimeTaken = TimeTaken
                };

                db.Images.Add(image);
                db.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Accepts an Image from the tablet.
        /// </summary>
        /// <returns>Success</returns>
        public Task<HttpResponseMessage> PostImage(int HitchBotID = 10, string timeTaken = "20150121000000")
        {
            DateTime TimeTaken = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads/img");
            var provider = new MultipartFormDataStreamProvider(root);

            var task = request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(o =>
                {

                    string file1 = provider.FileData.First().LocalFileName;
                    // this is the file name on the server where the file was saved this should be passed on to upload it to azure
                    Debug.WriteLine(file1);
                    Helpers.AzureBlobHelper.UploadImageAndAddToDb(HitchBotID, TimeTaken, "", file1);

                    return new HttpResponseMessage()
                    {
                        Content = new StringContent("File uploaded."),
                        StatusCode = HttpStatusCode.OK
                    };
                }
            );
            //TODO: Actually upload the image to azure blob (very easy code, the question is how to thread it).
            return task;
        }

        //public Task<HttpResponseMessage> PostImage()
        //{
        //    HttpRequestMessage request = this.Request;
        //    if (!request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads/img");
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    var task = request.Content.ReadAsMultipartAsync(provider).
        //        ContinueWith<HttpResponseMessage>(o =>
        //        {

        //            string file1 = provider.FileData.First().LocalFileName;
        //            // this is the file name on the server where the file was saved this should be passed on to upload it to azure

        //            //Helpers.AzureBlobHelper.UploadImageAndAddToDb(HitchBotID, TimeTaken, root, file1);

        //            return new HttpResponseMessage()
        //            {
        //                Content = new StringContent("File uploaded."),
        //                StatusCode = HttpStatusCode.OK
        //            };
        //        }
        //    );
        //    //TODO: Actually upload the image to azure blob (very easy code, the question is how to thread it).
        //    return task;
        //}

        [HttpPost]
        public bool AcceptImage(int ImageAcceptID)
        {
            using (var db = new Models.Database())
            {
                var img = db.Images.First(i => i.ID == ImageAcceptID);
                img.TimeApproved = DateTime.UtcNow;
                db.SaveChanges();
            }
            return true;
        }

        [HttpPost]
        public bool DenyImage(int ImageDenyID)
        {
            using (var db = new Models.Database())
            {
                var img = db.Images.First(i => i.ID == ImageDenyID);
                img.TimeDenied = DateTime.UtcNow;
                db.SaveChanges();
            }
            return true;
        }
    }
}