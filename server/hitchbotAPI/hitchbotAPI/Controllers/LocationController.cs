using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Helpers.LocationHelper.gmapsString + Helpers.LocationHelper.GetEncodedPolyLine(HitchBotID));
            return response;
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
        public bool UpdateHitchBotLocationMin(string HitchBot, string Latitude, string Longitude, string TakenTime)
        {
            DateTime StartTimeReal = DateTime.ParseExact(TakenTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Database())
            {
                int HitchBotIDint = int.Parse(HitchBot);
                double LatDouble = double.Parse(Latitude);
                double LongDouble = double.Parse(Longitude);
                var hitchBOT = db.hitchBOTs.First(h => h.ID == HitchBotIDint);
                var location = new Location();
                location.Latitude = LatDouble;
                location.Longitude = LongDouble;
                location.TakenTime = StartTimeReal;
                location.TimeAdded = DateTime.UtcNow;
                hitchBOT.Locations.Add(location);
                db.SaveChanges();
            }
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
        public bool UpdateHitchBotLocation(int HitchBotID, double Latitude, double Longitude, double Altitude, float Accuracy, float Velocity, DateTime TakenTime)
        {
            using (var db = new Database())
            {
                var hitchBOT = db.hitchBOTs.First(h => h.ID == HitchBotID);
                var location = new Location();
                location.Latitude = Latitude;
                location.Longitude = Longitude;
                location.Altitude = Altitude;
                location.Accuracy = Accuracy;
                location.Velocity = Velocity;
                location.TakenTime = TakenTime;
                location.TimeAdded = DateTime.UtcNow;
                hitchBOT.Locations.Add(location);
                db.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Get a Location instance by it's ID.
        /// </summary>
        /// <param name="ID">ID of the Location requested.</param>
        /// <returns>The requested Location instance.</returns>
        [HttpGet]
        public Location GetLocationByID(int ID)
        {
            return (new Database()).Locations.First(l => l.ID == ID);
        }
    }
}