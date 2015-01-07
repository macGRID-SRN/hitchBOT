using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Text;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class TargetLocationPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                Build_Javascript_Coords();
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void Build_Javascript_Coords()
        {
            using (var db = new Models.Database())
            {
                var user = (Models.Password)Session["New"];
                var hitchbot = db.hitchBOTs.Include(l => l.WikipediaEntries.Select(i => i.TargetLocation)).First(l => l.ID == user.hitchBOT.ID);

                var locations = hitchbot.WikipediaEntries.Where(l => l.TargetLocation != null).ToList();

                StringBuilder buildOutput = new StringBuilder();

                buildOutput.Append(@"<script type=""text/javascript"">");

                buildOutput.Append(@"var coords = [");

                buildOutput.Append(string.Join(",\n", locations.Select(coord => string.Format("{{ coord : new google.maps.LatLng({0},{1}), radius : {2} }}", coord.TargetLocation.Latitude, coord.TargetLocation.Longitude, coord.RadiusKM)).ToList()));

                buildOutput.Append(@"];");


                buildOutput.Append(@"</script>");

                coordsOutput.Text = buildOutput.ToString();
            }
        }
    }
}