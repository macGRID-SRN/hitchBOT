using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Helpers.Location
{
    public static class LocationHelper
    {
        public const string hBIcon = "http://goo.gl/uwnJCB";

        //yes you just found our API key.. wow .. congrats. Too bad it only works on hitchbot.me places.
        public const string gAPIkey = "&key=AIzaSyCEJOxq4a_fUiutFzCukico7aUy--6wMCw";

        public const string gmapsString = "http://maps.googleapis.com/maps/api/staticmap?size=800x800&path=weight:4%7Ccolor:blue%7Cenc:";
        public const string gmapsRegionString = "http://maps.googleapis.com/maps/api/geocode/json?latlng=";
        public const string gmapsMarkerString = "&markers=icon:http://goo.gl/uwnJCB|size:mid|color:red|label:H|";

        private const int maxLocations = 350;

       

        public static List<Models.Location> SlimLocations(List<Models.Location> inList)
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

            //always add the last value - map always updated then plus other things rely on it!
            outList.Add(inList.Last());

            return outList;
        }

        //code taken and modified from http://stackoverflow.com/questions/3852268/c-sharp-implementation-of-googles-encoded-polyline-algorithm
        //public static string EncodeCoordsForGMAPS(List<Models.Location> points)
        //{
        //    var str = new StringBuilder();

        //    var encodeDiff = (Action<int>)(diff =>
        //    {
        //        int shifted = diff << 1;
        //        if (diff < 0)
        //            shifted = ~shifted;
        //        int rem = shifted;
        //        while (rem >= 0x20)
        //        {
        //            str.Append((char)((0x20 | (rem & 0x1f)) + 63));
        //            rem >>= 5;
        //        }
        //        str.Append((char)(rem + 63));
        //    });

        //    int lastLat = 0;
        //    int lastLng = 0;
        //    foreach (var point in points)
        //    {
        //        int lat = (int)Math.Round(point.Latitude * 1E5);
        //        int lng = (int)Math.Round(point.Longitude * 1E5);
        //        encodeDiff(lat - lastLat);
        //        encodeDiff(lng - lastLng);
        //        lastLat = lat;
        //        lastLng = lng;
        //    }
        //    return str.ToString();
        //}
    }
}
