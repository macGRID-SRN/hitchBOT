using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Entity;
using System.Web.Caching;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Access
{
    public partial class PreviewMapCoverage : System.Web.UI.Page
    {
        protected int HitchBotId;

        private Location currentLocation;
        protected double lat { get { return currentLocation.Latitude; } }
        protected double lng { get { return currentLocation.Longitude; } }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack && Session["New"] != null)
            {
                HitchBotId = (int)Session[SessionInfo.HitchBotId];
                Build_Javascript_Coords();
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void Build_Javascript_Coords()
        {
            using (var db = new Dal.DatabaseContext())
            {
                currentLocation =
                    db.Locations.Where(l => l.HitchBotId == HitchBotId)
                        .Where(l => !l.HideFromProduction)
                        .Where(l => l.LocationProvider == LocationProvider.SpotGPS)
                        .OrderByDescending(l => l.TakenTime)
                        .First();

                var locations = db.CleverscriptContents
                    .Where(k => k.HitchBotId == HitchBotId && k.Location != null)
                    .Select(l =>
                        new
                        {
                            l.Location,
                            l.CleverText,
                            l.CleverscriptContext.HumanReadableBaseLabel,
                            l.Id,
                            l.EntryName,
                            l.RadiusKm,
                            l.isBucketList
                        }).ToList();

                var polys =
                    db.CleverscriptContents.Include(l => l.PolgonVertices)
                        .Where(l => l.HitchBotId == HitchBotId && l.Location == null)
                        .Select(l => new
                        {
                            poly = l.PolgonVertices.Select(a => a.Location),
                            l.CleverText,
                            l.CleverscriptContext.HumanReadableBaseLabel,
                            l.Id,
                            l.EntryName,
                            l.isBucketList
                        }).ToList();
                StringBuilder buildOutput = new StringBuilder();

                buildOutput.Append(@"<script type=""text/javascript"">");

                buildOutput.Append(@"var coords = [");

                buildOutput.Append(string.Join(",\n", locations.Select(coord => string.Format("{{ coord : new google.maps.LatLng({0},{1}), radius : {2}, title : '{3}', content : '{4}', bucketList: {5}}}", coord.Location.Latitude, coord.Location.Longitude,
                    coord.RadiusKm,
                    "Id: " + coord.Id + " - " + coord.EntryName + " - " + coord.HumanReadableBaseLabel,
                    EntryToParagraphs(coord.CleverText).Replace("'", "\'"), coord.isBucketList.ToString().ToLower())).ToList()));

                buildOutput.Append(@"];" + "\n");

                buildOutput.Append(@"var polys = [");


                buildOutput.Append(string.Join(",\n",
                    polys.Select(coords => string.Format("{{ coord: [{0}], title : '{1}', content : '{2}', bucketList: {3}}}",
                        string.Join(",", coords.poly.Select(loc => string.Format("new google.maps.LatLng({0},{1})", loc.Latitude, loc.Longitude))),
                        "Id: " + coords.Id + " - " + coords.EntryName + " - " + coords.HumanReadableBaseLabel,
                        EntryToParagraphs(coords.CleverText).Replace("'", "\'"), coords.isBucketList.ToString().ToLower()
                        ))));

                buildOutput.Append(@"];");

                buildOutput.Append(@"</script>");

                coordsOutput.Text = buildOutput.ToString();
            }
        }

        private string EntryToParagraphs(string entry)
        {
            entry = entry.Replace("'", "").Replace("\r", "");
            return "<p>" + string.Join("<p/><p>", entry.Split('\n')) + "<p/>";
        }
    }
}