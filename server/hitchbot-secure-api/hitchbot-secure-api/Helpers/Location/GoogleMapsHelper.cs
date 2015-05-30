using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Helpers.Location
{
    public static class GoogleMapsHelper
    {
        public static void BuildLocationJS(int HitchBotID)
        {
            using (var db = new Dal.DatabaseContext())
            {
                var locations = db.Locations.Where(l => l.HitchBotId == HitchBotID).OrderBy(l => l.TakenTime).ToList();

                var slimmedLocations = LocationHelper.SlimLocations(locations);
                string builder = "var flightPlanCoordinates = [ ";

                foreach (Models.Location myLocation in slimmedLocations)
                {
                    builder += "\n new google.maps.LatLng(" + myLocation.Latitude + "," + myLocation.Longitude + "),";
                }

                builder += @" ]; 

                var flightPath = new google.maps.Polyline({
                    path: flightPlanCoordinates,
                    geodesic: true,
                    strokeColor: '#E57373', //taken from material design by google
                    strokeOpacity: 1.0,
                    strokeWeight: 2
                });

                function AddPolyFill(map){
                    flightPath.setMap(map);
                }";

                System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, builder);
            }
        }

        private static string GenInfoWindow(Models.Location myLocation)
        {
            string returnString = @"function AddInfoWindow(map){ 
                    var myLatLong = new google.maps.LatLng(" + myLocation.Latitude + "," + myLocation.Longitude + @");
                    var contentString = '<div id=""content"">'+
                  '<div id=""siteNotice"">'+
                  '</div>'+
                  '<h1 id=""firstHeading"" class=""firstHeading"">Manitoulin Island</h1>'+
                  '<div id=""bodyContent"">'+
                  '<p>Canada’s most famous hitchhiking robot spent part of its holiday weekend taking part in a Pow Wow with the Wikwemikong First Nation on Manitoulin Island, picking up an honourary name in the process.'+
                  '<p>Attribution: National Post, <a href=""http://news.nationalpost.com/2014/08/04/hitchbot-update-canadas-hitchhiking-robot-picks-up-an-honourary-name-on-manitoulin-island/"">'+
                  'See the article here.</p>'+
                  '</div>'+
                  '</div>';

              var infowindow = new google.maps.InfoWindow({
                  content: contentString
              });

              var marker = new google.maps.Marker({
                  position: myLatLong,
                  map: map,
                  title: 'hitchBOT on Manitoulin Island'
              });
              google.maps.event.addListener(marker, 'click', function() {
                infowindow.open(map,marker);
              }); return infowindow;}";

            return returnString;
        }
    }
}
