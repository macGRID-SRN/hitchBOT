using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Globalization;
using System.Diagnostics;
using System.Net;

namespace hitchbot_secure_api.Controllers
{
    public class ImageController : ApiController
    {
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

            return task;
        }
    }
}
