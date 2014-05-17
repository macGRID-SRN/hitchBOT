using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    public class LocationController : ApiController
    {
        [HttpGet]
        public string GetGoogleMapsRoute(int HitchBotID)
        {
            using (var db = new Models.Database())
            {
                var OrderedLocations = db.hitchBOTs.First(h => h.ID == HitchBotID).Locations.OrderByDescending(l => l.TakenTime).ToList();

                return Helpers.LocationHelper.EncodeCoordsForGMAPS(OrderedLocations);
            }
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