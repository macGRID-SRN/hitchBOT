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
        public bool AddImage(int HitchBotID, int locationID, string timeTaken, string URL)
        {
            DateTime StartTimeReal = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchBOT = db.hitchBOTs.First(l => l.ID == HitchBotID);
                var location = db.Locations.First(l => l.ID == locationID);

                var image = new Models.Image()
                {
                    Location = location,
                    HitchBOT = hitchBOT,
                    url = URL
                };

                db.Images.Add(image);
                db.SaveChanges();
            }

            return true;
        }

        [HttpPost]
        public bool AddImage(int HitchBotID, string timeTaken, string URL)
        {
            DateTime StartTimeReal = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchBOT = db.hitchBOTs.First(l => l.ID == HitchBotID);

                var image = new Models.Image()
                {
                    HitchBOT = hitchBOT,
                    url = URL
                };

                db.Images.Add(image);
                db.SaveChanges();
            }

            return true;
        }

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