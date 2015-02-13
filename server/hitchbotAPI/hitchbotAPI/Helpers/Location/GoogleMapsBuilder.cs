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

        IList<Models.MapMarker> targets;

        static Models.Location DefaultLocation = new Models.Location { Latitude = 0, Longitude = 0, TakenTime = DateTime.UtcNow };
        Models.Location CenterLocation = DefaultLocation;
        //what gets counted here? only the markers which are start/stop or others. NOT info windows.
        int NumberOfMarkers = 0;

        string FunctionCalls = string.Empty;
        string BuilderEN = string.Empty;
        string BuilderDE = string.Empty;

        public GoogleMapsBuilder(int hitchbotID, int projectID = 3, int userID = 4)
        {
            using (var db = new Models.Database())
            {
                this.HitchBOT = db.hitchBOTs.Include(l => l.Locations).First(l => l.ID == hitchbotID);

                //if no projectID is given, will default to null
                this.Project = db.Projects.Include(l => l.StartLocation).Include(l => l.EndLocation).FirstOrDefault(l => l.ID == projectID);
                
                //if there was a project found, center the map around its start and end points.
                if (this.Project != null)
                {
                    if (this.Project.StartLocation != null && this.Project.EndLocation != null)
                        this.CenterLocation = new Models.Location
                        {
                            Latitude = (this.Project.StartLocation.Latitude + this.Project.EndLocation.Latitude) / 2,
                            Longitude = (this.Project.StartLocation.Longitude + this.Project.EndLocation.Longitude) / 2
                        };
                    // Get all target mapmarkers
                    this.targets = db.MapMarkers.Include(t => t.TargetLocation).Where(t => t.Project.ID == this.Project.ID).ToList();
                }

                //same deal as projectID, although the account does kind of come with a default hitchBOT.
                this.Account = db.Passwords.FirstOrDefault(l => l.ID == userID);
            }
        }

        /// <summary>
        /// This is the function which builds and saves the js code locally. It builds it based on how it was initialized, whether it was linked to a project and other factors.
        /// </summary>
        public void BuildJS()
        {
            this.BuilderEN = BuildPolyPath(0);
            this.BuilderDE = BuildPolyPath(0);

            this.BuilderEN += BuildCurrentLocation();
            this.BuilderDE += BuildCurrentLocation();

            if (this.Project != null)
            {
                this.BuilderEN += BuildTargets("en");
                this.BuilderDE += BuildTargets("de");
            }
            else
            {
                //handle null project
            }
            this.BuilderEN += BuildGoogleMapsJsBody();
            this.BuilderDE += BuildGoogleMapsJsBody();


            System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + "en" + this.HitchBOT.ID + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, this.BuilderEN);
            System.IO.File.WriteAllText(Helpers.PathHelper.GetJsBuildPath() + Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME + "de" + this.HitchBOT.ID + Helpers.AzureBlobHelper.JS_FILE_EXTENSION, this.BuilderDE);

        }

        public void BuildJsAndUpload()
        {
            BuildJS();

            Helpers.AzureBlobHelper.UploadJStoAzure(HitchBOT.ID, "en");
            Helpers.AzureBlobHelper.UploadJStoAzure(HitchBOT.ID, "de");            
            
        }

        private string BuildCurrentLocation()
        {
            var current_loc = this.HitchBOT.Locations.Where(l => l.TakenTime > new DateTime(2014, 07, 27, 13, 30, 0)).OrderBy(l => l.TakenTime).ToArray().LastOrDefault();
            return @"
                    var currentPoint = new google.maps.LatLng(" + current_loc.Latitude + "," + current_loc.Longitude + @");
                    ";

        }

        private string BuildTargets(string lang, Models.Location myLocation = null)
        {
            string builder = string.Empty;

            if (targets != null)
            {
                if (targets.Count() > 0)
                {

                    builder = "var targetCoordinates = [ ";

                    // number to display on the markers. Increments every time.
                    var num = 1;
                    foreach (Models.MapMarker l in targets)
                    {

                        if (l.TargetLocation != null)
                        {
                            if (lang == "en")
                            {
                                // Create marker detail for English
                                builder += "{LatLng: new google.maps.LatLng(" + l.TargetLocation.Latitude + ", ";
                                builder += l.TargetLocation.Longitude + "), info_text: {info_header: '" + new System.Web.HtmlString(l.HeaderText);
                                builder += "', info_body: '" + new System.Web.HtmlString(l.BodyText) + "'}, touched: ";
                                builder += l.HasBeenVisited ? "true" : "false";
                                builder += ", number: " +num+ "},";
                            }
                            else
                            {
                                // Create marker detail for German
                                builder += "{LatLng: new google.maps.LatLng(" + l.TargetLocation.Latitude + ", ";
                                builder += l.TargetLocation.Longitude + "), info_text: {info_header: '" + new System.Web.HtmlString(l.HeaderTextGerman);
                                builder += "', info_body: '" + new System.Web.HtmlString(l.BodyTextGerman) + "'}, touched: ";
                                builder += l.HasBeenVisited ? "true" : "false";
                                builder += ", number: " +num+ "},";
                            }
                            // Increment marker number
                            num = num + 1;
                        }
                    }
                    // Close locations JS array
                    builder += "];";
                }
                else
                {
                    // If there's no locations, define it as empty
                    builder = "var targetCoordinates = []; ";
                }
            }
            return builder;
        }

        private string BuildGoogleMapsJsBody()
        {
            return @"function addTargetMarkers(e){pinColor_touched=""65ba4a"";pinColor_untouched=""ff796c"";var t=[];for(o=0;o<targetCoordinates.length;o++){var n=targetCoordinates[o];if(n.touched==false||n.touched==""false""||n.touched==0||n.touched==""no""||typeof n.touched==""undefined""){n.touched=false}else{n.touched=true}var r=new google.maps.MarkerImage(""http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=""+n.number.toString()+""|""+(n.touched==true?pinColor_touched:pinColor_untouched)+""|000000"",new google.maps.Size(21,34),new google.maps.Point(0,0),new google.maps.Point(10,34));var i=""<div id='content'>""+""<div id='siteNotice'></div>""+""<h1 id='firstHeading' class='firstHeading'>""+n.info_text.info_header+""</h1>""+""<div id='bodyContent'><p>""+n.info_text.info_body+""</p></div></div>"";var s=new google.maps.Marker({position:n.LatLng,map:e,title:n.info_text.info_header,html:i,icon:r});t.push(s)}for(var o=0;o<t.length;o++){var s=t[o];google.maps.event.addListener(s,""click"",function(){infowindow.setContent(this.html);infowindow.close();infowindow.open(e,this)})}}function AddPolyFill0(e){var t=new google.maps.Polyline({path:flightPlanCoordinates,geodesic:true,strokeColor:""#E57373"",strokeOpacity:1,strokeWeight:4});t.setMap(e)}function AddCurrentMarker(e){var t=new google.maps.MarkerImage(""http://hitchbotimg.blob.core.windows.net/img/hitchicon2.png"",new google.maps.Size(64,64),new google.maps.Point(0,0),new google.maps.Point(10,34));var n=new google.maps.Marker({position:currentPoint,map:e,animation:google.maps.Animation.DROP,icon:t});return}function AutoCenter(e){var t=new google.maps.LatLngBounds;t.extend(currentPoint);for(i=0;i<targetCoordinates.length;i++){t.extend(targetCoordinates[i].LatLng)}e.fitBounds(t)}function initialize(){var e={center:centerPoint,zoom:centerZoom};var t=new google.maps.Map(document.getElementById(""map-canvas""),e);google.maps.event.addListener(t,""click"",function(){if(infowindow.getMap()!==null&&typeof infowindow.getMap()!==""undefined""){infowindow.close()}});AddPolyFill0(t);if(autocenter){AutoCenter(t)}addTargetMarkers(t);AddCurrentMarker(t)}var centerPoint=new google.maps.LatLng(48.1384,11.573399999999992);var centerZoom=9;var autocenter=false;var infowindow=new google.maps.InfoWindow({content:"" "",maxWidth:400});google.maps.event.addDomListener(window,""load"",initialize)";
        }

        /// <summary>
        /// Builds the function for a path on a google map. It loads the data from the hitchBOT entity and builds it from its locations.
        /// </summary>
        /// <returns></returns>
        private string BuildPolyPath(int PolyPathNumber)
        {
            //get all the locations from before launch--messy TODO update this for the current launch date
            var locations = this.HitchBOT.Locations.Where(l => l.TakenTime > new DateTime(2014, 07, 27, 13, 30, 0)).OrderBy(l => l.TakenTime).ToList();

            string builder = string.Empty;

            if (locations != null)
            {
                if (locations.Count > 0)
                {
                    var slimmedLocations = LocationHelper.SlimLocations(locations);

                    builder = "var flightPlanCoordinates = [ ";

                    // Slim locations
                    foreach (Models.Location myLocation in slimmedLocations)
                    {
                        // Add each location to a JS array.
                        builder += "\n new google.maps.LatLng(" + myLocation.Latitude + "," + myLocation.Longitude + "), ";
                    }

                    builder += @"];
                    ";
                }
                else
                {
                    builder = @"var flightPlanCoordinates = [];
                    ";
                }
            }
            return builder;
        }

    }
}
