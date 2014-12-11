﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;

namespace hitchbotAPI.Helpers.Location
{
    public static class GoogleMapsHelper
    {
        public static void BuildLocationJS(int HitchBotID)
        {
            using (var db = new Models.Database())
            {
                var locations = db.hitchBOTs.Include(l => l.Locations).SingleOrDefault(l => l.ID == HitchBotID).Locations.Where(l => l.TakenTime > new DateTime(2014, 07, 27, 13, 30, 0)).OrderBy(l => l.TakenTime).ToList();

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

                builder += GenStartMarker(locations.First());
                builder += GenEndMarker(locations.Last());
                builder += GenHbMarker(new Models.Location { Latitude = 43.7000, Longitude = -79.4000 });
                builder += GenInfoWindow(new Models.Location { Latitude = 45.7667, Longitude = -82.2000 });
                builder += GenGoogleMapsInit();

                System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME, builder);
            }
        }

        public static string GenGoogleMapsInit()
        {
            return @"function initialize() {
                        var mapOptions = {
                          center: { lat: 48.3822, lng: -89.2461},
                          zoom: 4
                        };
                        var map = new google.maps.Map(document.getElementById('map-canvas'),
                            mapOptions);
         
                          //generated by the server every set period of time!
                        AddPolyFill(map);
                        AddStartMarker(map);
                        AddEndMarker(map);
                        AddHbMarker(map);
                        AddInfoWindow(map);
                      }
                      google.maps.event.addDomListener(window, 'load', initialize);";
        }

        public static void WriteJavaScriptFileLocal(string fileContents, int hitchBotID)
        {

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

        private static string GenStartMarker(Models.Location myLocation)
        {
            return GenColouredMarker(myLocation, "65ba4a", "AddStartMarker");
        }

        private static string GenEndMarker(Models.Location myLocation)
        {
            return GenColouredMarker(myLocation, "ff796c", "AddEndMarker");
        }

        private static string GenHbMarker(Models.Location myLocation)
        {
            string returnString = @"function AddHbMarker(map){

                var pinImage = new google.maps.MarkerImage('" + Helpers.LocationHelper.hBIcon + @"');

                var startMarker = new google.maps.Marker({
                position: new google.maps.LatLng(";

            returnString += myLocation.Latitude + "," + myLocation.Longitude + "),";

            returnString += @"
            map: map,
            animation: google.maps.Animation.DROP,
            icon: pinImage";

            returnString += "}); }";

            return returnString;
        }

        private static string GenColouredMarker(Models.Location myLocation, string colour, string markerFunctionName)
        {
            string returnString = "function " + markerFunctionName + @"(map){

                var pinColor = '" + colour + @"';
                var pinImage = new google.maps.MarkerImage('http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|' + pinColor,
                    new google.maps.Size(21, 34),
                    new google.maps.Point(0,0),
                    new google.maps.Point(10, 34));
                var pinShadow = new google.maps.MarkerImage('http://chart.apis.google.com/chart?chst=d_map_pin_shadow',
                    new google.maps.Size(40, 37),
                    new google.maps.Point(0, 0),
                    new google.maps.Point(12, 35));

                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(";

            returnString += myLocation.Latitude + "," + myLocation.Longitude + "),";

            returnString += @"
            map: map,
            animation: google.maps.Animation.DROP,
            icon: pinImage,
            shadow: pinShadow";

            returnString += "}); return marker;}";

            return returnString;
        }

    }
}
