using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Globalization;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    public class LocationController : ApiController
    {
        /// <summary>
        /// Redirects to a static GMAPS image which is the total path of a HitchBot. Note: We can only build this map 25,000 times per day.
        /// </summary>
        /// <param name="HitchBotID">The HitchBot to get the route of.</param>
        /// <returns>Redirects to the proper link.</returns>
        [HttpGet]
        public HttpResponseMessage GetGoogleMapsRoute(int HitchBotID)
        {
            using (var db = new Models.Database())
            {
                var response = Request.CreateResponse(HttpStatusCode.Moved);

                var mapsURL = db.StaticMaps.Where(sm => sm.HitchBot.ID == HitchBotID);
                if (mapsURL.Count() > 0)
                {
                    var lastGenerated = mapsURL.OrderByDescending(l => l.TimeGenerated).First();
                    if (DateTime.UtcNow - lastGenerated.TimeGenerated > TimeSpan.FromHours(0.5))
                    {
                        response.Headers.Location = new Uri(GetStaticMapURL(Helpers.LocationHelper.GetEncodedPolyLine(HitchBotID)));
                    }
                    else
                        response.Headers.Location = new Uri(GetStaticMapURL(lastGenerated.URL));
                }
                else
                    response.Headers.Location = new Uri(GetStaticMapURL(Helpers.LocationHelper.GetEncodedPolyLine(HitchBotID)));

                return response;
            }
        }

        /// <summary>
        /// Gets the static map API url (generate it)
        /// </summary>
        /// <param name="poly">The poly thing generated from somewhere else.. don't really know where. What was I thinking when I wrote this description? Who knows.</param>
        /// <returns></returns>
        private string GetStaticMapURL(string poly)
        {
            return Helpers.LocationHelper.gmapsString + poly + Helpers.LocationHelper.gAPIkey;
        }

        [HttpGet]
        public void BuildJS(int hitchBotIdJS)
        {
            var builer = new Helpers.Location.GoogleMapsBuilder(hitchBotIdJS);
            builer.BuildJsAndUpload();
        }

        private string GetRegionText(int HitchBotID)
        {

            return string.Empty;
        }

        /// <summary>
        /// Adds a Locations with the bare minimum of data required for a complete entry
        /// </summary>
        /// <param name="HitchBotID">The HitchBot to add a new Location to.</param>
        /// <param name="Latitude">Latitude</param>
        /// <param name="Longitude">Longitude</param>
        /// <param name="TakenTime">TakenTime</param>
        /// <returns>Success</returns>
        [HttpPost]
        public bool UpdateHitchBotLocationMin(string HitchBotID, string Latitude, string Longitude, string TakenTime)
        {
            DateTime StartTimeReal = DateTime.ParseExact(TakenTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            int newLocationID;
            int hitchBotID;
            using (var db = new Models.Database())
            {
                hitchBotID = int.Parse(HitchBotID);
                double LatDouble = double.Parse(Latitude);
                double LongDouble = double.Parse(Longitude);
                var hitchBOT = db.hitchBOTs.Include(h => h.Locations).First(h => h.ID == hitchBotID);
                var location = new Location()
                {
                    Latitude = LatDouble,
                    Longitude = LongDouble,
                    TakenTime = StartTimeReal,
                    TimeAdded = DateTime.UtcNow,
                    HitchBOT = hitchBOT
                };

                db.Locations.Add(location);
                db.SaveChanges();
                //hitchBOT.Locations.Add(location);
                //db.SaveChanges();

                newLocationID = location.ID;
            }

            //Helpers.LocationHelper.CheckForTargetLocation(hitchBotID, newLocationID);
            return true;
        }
        /// <summary>
        /// Given the ID of a HitchBot, add it's location.
        /// </summary>
        /// <param name="HitchBotID">The HitchBot to add a new Location to.</param>
        /// <param name="Latitude">Latitude</param>
        /// <param name="Longitude">Longitude</param>
        /// <param name="Altitude">Altitude</param>
        /// <param name="Accuracy">Accuracy</param>
        /// <param name="Velocity">Velocity</param>
        /// <param name="TakenTime">TakenTime</param>
        /// <returns>Success</returns>
        [HttpPost]
        public bool UpdateHitchBotLocation(int HitchBotID, double Latitude, double Longitude, double Altitude, float Accuracy, float Velocity, string TakenTime)
        {
            DateTime StartTimeReal = DateTime.ParseExact(TakenTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchBOT = db.hitchBOTs.Include(l => l.Locations).First(h => h.ID == HitchBotID);
                var location = new Location();
                location.Latitude = Latitude;
                location.Longitude = Longitude;
                location.Altitude = Altitude;
                location.Accuracy = Accuracy;
                location.Velocity = Velocity;
                location.TakenTime = StartTimeReal;
                location.TimeAdded = DateTime.UtcNow;
                location.HitchBOT = hitchBOT;
                db.Locations.Add(location);
                db.SaveChanges();
                hitchBOT.Locations.Add(location);
                db.SaveChanges();
            }
            return true;
        }
    }
}