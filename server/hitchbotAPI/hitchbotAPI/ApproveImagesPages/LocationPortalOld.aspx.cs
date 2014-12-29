using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class LocationPortalOld : System.Web.UI.Page
    {

        private int HitchBotID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    this.HitchBotID = user.hitchBOT.ID;
                    var lastMap = db.StaticMaps.Include(l => l.HitchBot).Where(l => l.HitchBot.ID == this.HitchBotID).OrderBy(l => l.TimeGenerated).ToList().Last();
                    var lastLoc = db.Locations.Include(l => l.HitchBOT).Where(l => l.HitchBOT.ID == this.HitchBotID).OrderBy(l => l.TakenTime).ToList().Last();

                    lblLocation.Text += lastLoc.Latitude + ", " + lastLoc.Longitude;
                    lblGmapsOutput.Text += string.IsNullOrWhiteSpace(lastMap.NearestCity) ? "Unable to Resolve" : lastMap.NearestCity;
                    lblTime.Text += lastLoc.TakenTime.ToString() + " (UTC)";
                    lblVelocity.Text += lastLoc.Velocity == null ? "Not Reported" : string.Empty + lastLoc.Velocity + " m/s";
                    lblGen.Text += lastMap.TimeGenerated.ToString() + " (UTC)";
                    Button1.Click += this.Button_Force_Generate;
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        private void Button_Force_Generate(object sender, System.EventArgs e)
        {
            Helpers.LocationHelper.GetEncodedPolyLine(this.HitchBotID);

            Response.Redirect("LocationPortalOld.aspx", true);
        }
    }
}