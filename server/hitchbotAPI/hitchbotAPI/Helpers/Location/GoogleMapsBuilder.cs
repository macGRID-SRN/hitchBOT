using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace hitchbotAPI.Helpers.Location
{
    /// <summary>
    /// This class is used to build the JS for the Dynamic Map on the HB website. Why doesn't this class take in the actual items and instead access the db? It could be called from any context.
    /// </summary>
    public class GoogleMapsBuilder
    {
        //these "map options" should be loaded from the db in the future.
        private const string PolyPathColour = "E57373";
        private const double PolyPathOpacity = 1.0;
        private const int PolyPathStrokeWeight = 2;
        private const int DefaultMapZoomLevel = 4;

        Models.hitchBOT HitchBOT;
        Models.Project Project;
        Models.Password Account;
        Models.Location CenterLocation;
        Models.Location DefaulLocation = new Models.Location { Latitude = 0, Longitude = 0, TakenTime = DateTime.UtcNow };

        //what gets counted here? only the markers which are start/stop or others. NOT info windows.
        int NumberOfMarkers = 0;

        string FunctionCalls = string.Empty;
        string Builder = string.Empty;

        public GoogleMapsBuilder(int hitchbotID, int projectID = 0, int userID = 0)
        {
            using (var db = new Models.Database())
            {
                this.HitchBOT = db.hitchBOTs.Include(l => l.Locations).First(l => l.ID == hitchbotID);

                //if no projectID is given, will default to null
                this.Project = db.Projects.Include(l => l.StartLocation).Include(l => l.EndLocation).FirstOrDefault(l => l.ID == projectID);

                //same deal as projectID, although the account does kind of come with a default hitchBOT.
                this.Account = db.Passwords.FirstOrDefault(l => l.ID == userID);
            }
        }

        public void BuildJS()
        {
            this.Builder += BuildPolyPath(0);

            if (this.Project != null)
            {
                this.Builder += BuildStartLocation();
                this.Builder += BuildEndLocation();
            }
            else
            {
                this.Builder += BuildStartLocation(HitchBOT.Locations.FirstOrDefault());
            }

            this.Builder += BuildGoogleMapsInit();

            //that is a nice path wow.
            System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + this.HitchBOT.ID + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, this.Builder);
        }

        public void BuildJsAndUpload()
        {
            BuildJS();
            Helpers.AzureBlobHelper.UploadJStoAzure(HitchBOT.ID);
        }

        private string BuildStartLocation(Models.Location myLocation = null)
        {
            const string startLocationFunctionName = "AddStartMarker";
            const string startLocationMarkerColour = "65ba4a";

            var location = myLocation ?? this.Project.StartLocation ?? this.DefaulLocation;

            return this.BuildColouredMarker(location, startLocationMarkerColour, startLocationFunctionName);
        }

        private string BuildEndLocation(Models.Location myLocation = null)
        {
            const string startLocationFunctionName = "AddEndMarker";
            const string startLocationMarkerColour = "ff796c";

            var location = myLocation ?? this.Project.EndLocation ?? this.DefaulLocation;

            return this.BuildColouredMarker(location, startLocationMarkerColour, startLocationFunctionName);
        }

        private string BuildColouredMarker(Models.Location myLocation, string colour, string markerFunctionName)
        {
            string returnString = "function " + markerFunctionName + this.NumberOfMarkers + @"(map){

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

            this.AddFunctionCall(markerFunctionName, this.NumberOfMarkers++);

            return returnString;
        }

        private string BuildGoogleMapsInit()
        {
            return @"function initialize() {
                        var mapOptions = {
                          center: { lat: 48.3822, lng: -89.2461},
                          zoom: 4
                        };
                        var map = new google.maps.Map(document.getElementById('map-canvas'),
                            mapOptions);
         
                        //generated by the server every set period of time!
                        " + this.FunctionCalls + @"
                      }
                      google.maps.event.addDomListener(window, 'load', initialize);";
        }

        /// <summary>
        /// Builds the function for a path on a google map. It loads the data from the hitchBOT entity and builds it from its locations.
        /// </summary>
        /// <param name="PolyPathNumber">If there were to be more than one hitchBOTS, you can number these functions.</param>
        /// <returns></returns>
        private string BuildPolyPath(int PolyPathNumber)
        {
            const string functionName = "AddPolyFill";
            //get all the locations from before launch--messy
            var locations = this.HitchBOT.Locations.Where(l => l.TakenTime > new DateTime(2014, 07, 27, 13, 30, 0)).OrderBy(l => l.TakenTime).ToList();

            string builder = string.Empty;

            if (locations != null)
            {
                if (locations.Count > 0)
                {
                    var slimmedLocations = LocationHelper.SlimLocations(locations);

                    builder = "var flightPlanCoordinates = [ ";

                    foreach (Models.Location myLocation in slimmedLocations)
                    {
                        builder += "\n new google.maps.LatLng(" + myLocation.Latitude + "," + myLocation.Longitude + "),";
                    }

                    builder += @" ]; 

                var flightPath = new google.maps.Polyline({
                    path: flightPlanCoordinates,
                    geodesic: true,
                    strokeColor: '#" + PolyPathColour + @"', //taken from material design by google
                    strokeOpacity: " + PolyPathOpacity + @",
                    strokeWeight: " + PolyPathStrokeWeight + @"
                });

                function " + functionName + PolyPathNumber + @"(map){
                    flightPath.setMap(map);
                }";

                    this.AddFunctionCall(functionName, PolyPathNumber);
                }
            }
            return builder;
        }

        private void AddFunctionCall(string functionName, int functionNumber)
        {
            this.FunctionCalls += "\t" + functionName + functionNumber.ToString() + "(map);";
        }
    }
}
