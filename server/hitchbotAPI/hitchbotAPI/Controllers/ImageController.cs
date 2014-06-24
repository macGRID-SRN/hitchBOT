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
using LinqToTwitter;
using System.Threading.Tasks;

namespace hitchbotAPI.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        public bool AddImage(int locationID, string timeTaken)
        {
            DateTime StartTimeReal = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var location = db.Locations.First(l => l.ID == locationID);

                var image = new Models.Image()
                {
                    location = location
                };
            }

            return true;
        }

        [HttpPost]
        public bool AcceptImage(int ImageID)
        {
            using (var db = new Models.Database())
            {
                var img = db.Images.First(i => i.ID == ImageID);
                img.TimeApproved = DateTime.UtcNow;
                db.SaveChanges();
            }
            return true;
        }

        [HttpPost]
        public bool AcceptImage(int ImageID)
        {
            using (var db = new Models.Database())
            {
                var img = db.Images.First(i => i.ID == ImageID);
                img.TimeDenied = DateTime.UtcNow;
                db.SaveChanges();
            }
            return true;
        }
    }
}