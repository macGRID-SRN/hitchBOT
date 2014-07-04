using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Device.Location;
using System.Threading.Tasks;
using System.Data.Entity;
using LinqToTwitter;

namespace hitchbotAPI.Helpers
{
    public static class LocationHelper
    {
        public static async void CheckForTargetLocation(int HitchBotID, int LocationID)
        {
            using (var db = new Models.Database())
            {
                var location = db.Locations.First(l => l.ID == LocationID);
                var hitchbot = db.hitchBOTs.First(h => h.ID == HitchBotID);
                var TargetLocations = db.TwitterLocations.Where(l => l.HitchBot.ID == HitchBotID && l.Status == null).Include(l => l.TargetLocation);

                foreach (hitchbotAPI.Models.TwitterLocationTarget thisTargetLocation in TargetLocations)
                {
                    var currentLocation = new GeoCoordinate(location.Latitude, location.Longitude);
                    var targetLocation = new GeoCoordinate(thisTargetLocation.TargetLocation.Latitude, thisTargetLocation.TargetLocation.Longitude);
                    if (currentLocation.GetDistanceTo(targetLocation) <= thisTargetLocation.RadiusKM * 1000)
                    {
                        int TweetID = await Helpers.TwitterHelper.PostTweetWithLocation(HitchBotID, LocationID, thisTargetLocation.TweetText);
                        Task<int> WeatherTweet = TwitterHelper.PostTweetWithLocationAndWeather(HitchBotID, LocationID);
                        LinkTargetLocationToTweet(TweetID, thisTargetLocation.ID);
                        await WeatherTweet;
                        break;
                    }
                }

            }

        }

        private static void LinkTargetLocationToTweet(int TweetID, int TargetLocationID)
        {
            using (var db = new Models.Database())
            {
                var status = db.TwitterStatuses.First(ts => ts.ID == TweetID);
                var Target = db.TwitterLocations.First(tl => tl.ID == TargetLocationID);
                Target.Status = status;
                db.SaveChanges();
            }
        }

        public const string gmapsString = "http://maps.googleapis.com/maps/api/staticmap?size=800x800&path=weight:5%7Ccolor:blue%7Cenc:";
        private const int maxLocations = 200;

        public static string GetEncodedPolyLine(int HitchBotID)
        {
            using (var db = new Models.Database())
            {
                var OrderedLocations = db.hitchBOTs.Include(h => h.Locations).First(h => h.ID == HitchBotID).Locations.OrderBy(l => l.TakenTime).ToList();

                string tempURL = EncodeCoordsForGMAPS(SlimLocations(OrderedLocations));
                var hitchBOT = db.hitchBOTs.First(h => h.ID == HitchBotID);
                var tempStaticLink = new Models.GoogleMapsStatic()
                {
                    HitchBot = hitchBOT,
                    URL = tempURL,
                    TimeGenerated = DateTime.UtcNow,
                    TimeAdded = DateTime.UtcNow

                };

                db.StaticMaps.Add(tempStaticLink);
                db.SaveChanges();

                return tempURL;
            }
        }

        private static List<Models.Location> SlimLocations(List<Models.Location> inList)
        {
            int Interval = inList.Count / (LocationHelper.maxLocations - 2);

            if (Interval < 2)
            {
                Interval = 2;
            }
            List<Models.Location> outList = new List<Models.Location>();

            for (int i = 0; i < inList.Count; i += Interval)
            {
                outList.Add(inList[i]);
            }

            outList.Add(inList.Last());

            return inList;
        }

        //code taken and modified from http://stackoverflow.com/questions/3852268/c-sharp-implementation-of-googles-encoded-polyline-algorithm
        public static string EncodeCoordsForGMAPS(List<Models.Location> points)
        {
            var str = new StringBuilder();

            var encodeDiff = (Action<int>)(diff =>
            {
                int shifted = diff << 1;
                if (diff < 0)
                    shifted = ~shifted;
                int rem = shifted;
                while (rem >= 0x20)
                {
                    str.Append((char)((0x20 | (rem & 0x1f)) + 63));
                    rem >>= 5;
                }
                str.Append((char)(rem + 63));
            });

            int lastLat = 0;
            int lastLng = 0;
            foreach (var point in points)
            {
                int lat = (int)Math.Round(point.Latitude * 1E5);
                int lng = (int)Math.Round(point.Longitude * 1E5);
                encodeDiff(lat - lastLat);
                encodeDiff(lng - lastLng);
                lastLat = lat;
                lastLng = lng;
            }
            return str.ToString();
        }
    }
}