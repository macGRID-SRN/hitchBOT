using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data.Entity;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Helpers.Location
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

        Models.Journey Project;
        private int hitchBotId;

        static Models.Location DefaultLocation = new Models.Location { Latitude = 0, Longitude = 0, TakenTime = DateTime.UtcNow };
        Models.Location CenterLocation = DefaultLocation;
        //what gets counted here? only the markers which are start/stop or others. NOT info windows.
        int NumberOfMarkers = 0;

        string FunctionCalls = string.Empty;
        string BuilderEN = string.Empty;

        public GoogleMapsBuilder(int hitchBotId, int projectID = 1, int userID = 4)
        {
            this.hitchBotId = hitchBotId;
        }

        /// <summary>
        /// This is the function which builds and saves the js code locally. It builds it based on how it was initialized, whether it was linked to a project and other factors.
        /// </summary>
        public void BuildJS()
        {
            this.BuilderEN = BuildPolyPath(0);

            this.BuilderEN += BuildCurrentLocation();

            this.BuilderEN += BuildGoogleMapsJsBody();

            this.BuilderEN += BuildTargets();

            System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + "en" + this.hitchBotId + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, this.BuilderEN);

        }

        public void BuildJsAndUpload()
        {
            BuildJS();

            Helpers.AzureBlobHelper.UploadJStoAzure(hitchBotId, "en");

        }

        private string BuildCurrentLocation()
        {
            using (var db = new Dal.DatabaseContext())
            {
                var current_loc =
                    db.Locations.Where(l => l.HitchBotId == hitchBotId)
                        .Where(l => l.LocationProvider == LocationProvider.SpotGPS)
                        .OrderByDescending(l => l.TakenTime)
                        .FirstOrDefault();
                return @"var currentPoint = new google.maps.LatLng(" + current_loc.Latitude + "," + current_loc.Longitude + ");";
            }
        }

        private string BuildTargets()
        {
            string builder = "var targetCoordinates = []; ";


            //if (targets != null)
            //{
            //    if (targets.Count() > 0)
            //    {

            //        builder = "var targetCoordinates = [ ";

            //        // number to display on the markers. Increments every time.
            //        var num = 1;
            //        foreach (Models.MapMarker l in targets)
            //        {

            //            if (l.TargetLocation != null)
            //            {
            //                if (lang == "en")
            //                {
            //                    // Create marker detail for English
            //                    builder += "{LatLng: new google.maps.LatLng(" + l.TargetLocation.Latitude + ", ";
            //                    builder += l.TargetLocation.Longitude + "), info_text: {info_header: '" + new System.Web.HtmlString(l.HeaderText);
            //                    builder += "', info_body: '" + new System.Web.HtmlString(l.BodyText) + "'}, touched: ";
            //                    builder += l.HasBeenVisited ? "true" : "false";
            //                    builder += ", number: " + num + "},";
            //                }
            //                else
            //                {
            //                    // Create marker detail for German
            //                    builder += "{LatLng: new google.maps.LatLng(" + l.TargetLocation.Latitude + ", ";
            //                    builder += l.TargetLocation.Longitude + "), info_text: {info_header: '" + new System.Web.HtmlString(l.HeaderTextGerman);
            //                    builder += "', info_body: '" + new System.Web.HtmlString(l.BodyTextGerman) + "'}, touched: ";
            //                    builder += l.HasBeenVisited ? "true" : "false";
            //                    builder += ", number: " + num + "},";
            //                }
            //                // Increment marker number
            //                num = num + 1;
            //            }
            //        }
            //        // Close locations JS array
            //        builder += "];";
            //    }
            //    else
            //    {
            //        // If there's no locations, define it as empty
            //        builder = "var targetCoordinates = []; ";
            //    }
            //}
            return builder;
        }

        private string BuildGoogleMapsJsBody()
        {
            var stringy =
@"function AddPolyFill0(e){
	var t=new google.maps.Polyline({
		path:flightPlanCoordinates,
		geodesic:true,
		strokeColor:""#E57373"",
		strokeOpacity:1,
		strokeWeight:4
	});
	t.setMap(e)
}

function AddCurrentMarker(e){
	var t=new google.maps.MarkerImage(""http://hitchbotimg.blob.core.windows.net/img/hitchicon2.png"",
		new google.maps.Size(64,64),
		new google.maps.Point(0,0),
		new google.maps.Point(32,64));

	var n=new google.maps.Marker({
		position:currentPoint,
		map:e,
		animation:google.maps.Animation.DROP,
		icon:t});
		return;
}
function AutoCenter(e){
	var t=new google.maps.LatLngBounds;
	t.extend(currentPoint);
	for(i=0;i<flightPlanCoordinates.length;i++){
		t.extend(flightPlanCoordinates[i])
	}
	e.fitBounds(t)
}

function initialize(){

var e={
	center:centerPoint,
	zoom:centerZoom
};
var t=new google.maps.Map(document.getElementById(""map-canvas""),e);

AddPolyFill0(t);

AddCurrentMarker(t);
if(autocenter){AutoCenter(t)}

}

var centerPoint=new google.maps.LatLng(48.1384,11.573399999999992);

var centerZoom=10;
var autocenter=true;
google.maps.event.addDomListener(window,""load"",initialize);";

            return stringy;
        }

        //function addTargetMarkers(e){pinColor_touched=""65ba4a"";pinColor_untouched=""ff796c"";var t=[];for(o=0;o<targetCoordinates.length;o++){var n=targetCoordinates[o];if(n.touched==false||n.touched==""false""||n.touched==0||n.touched==""no""||typeof n.touched==""undefined""){n.touched=false}else{n.touched=true}var r=new google.maps.MarkerImage(""http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=""+n.number.toString()+""|""+(n.touched==true?pinColor_touched:pinColor_untouched)+""|000000"",new google.maps.Size(21,34),new google.maps.Point(0,0),new google.maps.Point(10,34));var i=""<div id='content'>""+""<div id='siteNotice'></div>""+""<h1 id='firstHeading' class='firstHeading'>""+n.info_text.info_header+""</h1>""+""<div id='bodyContent'><p>""+n.info_text.info_body+""</p></div></div>"";var s=new google.maps.Marker({position:n.LatLng,map:e,title:n.info_text.info_header,html:i,icon:r});t.push(s)}for(var o=0;o<t.length;o++){var s=t[o];google.maps.event.addListener(s,""click"",function(){infowindow.setContent(this.html);infowindow.close();infowindow.open(e,this)})}}

        /// <summary>
        /// Builds the function for a path on a google map. It loads the data from the hitchBOT entity and builds it from its locations.
        /// </summary>
        /// <returns></returns>
        private string BuildPolyPath(int PolyPathNumber)
        {
            //get all the locations from before launch--messy TODO update this for the current launch date
            using (var db = new Dal.DatabaseContext())
            {
                var locations = db.Locations
                    .Where(l => 
                        ((l.HitchBotId == hitchBotId && l.TakenTime > l.HitchBot.Journey.StartTime && l.LocationProvider == LocationProvider.SpotGPS) 
                        || (l.ForceProduction)) 
                        
                        && !l.HideFromProduction)
                    .OrderBy(l => l.TakenTime).ToList();

                string builder = string.Empty;

                if (locations.Count > 0)
                {
                    builder = "var flightPlanCoordinates = [ ";

                    var slimmedLocations = LocationHelper.SlimLocations(locations)
                        .Select(l => string.Format("new google.maps.LatLng({0},{1})", l.Latitude, l.Longitude));

                    builder += string.Join(",\n", slimmedLocations);

                    builder += @"];";
                }
                else
                {
                    builder = @"var flightPlanCoordinates = [];";
                }
                return builder;
            }
        }

    }
}
