using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace hitchbotAPI.Helpers
{
    public class LocationHelper
    {
        public const string gmapsString = "http://maps.googleapis.com/maps/api/staticmap?size=800x800&path=weight:3%7Ccolor:red%7Cenc:";
        public static string GetEncodedPolyLine(int HitchBotID)
        {
            using (var db = new Models.Database())
            {
                var OrderedLocations = db.hitchBOTs.First(h => h.ID == HitchBotID).Locations.OrderBy(l => l.TakenTime).ToList();

                return EncodeCoordsForGMAPS(OrderedLocations);
            }
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