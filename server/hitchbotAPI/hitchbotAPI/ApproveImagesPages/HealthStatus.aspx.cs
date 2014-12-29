using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class HealthStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    var status = db.TabletStatusI.Where(ts => ts.HitchBOT.ID == hitchBOTid).OrderByDescending(l => l.TimeTaken).ToList().First();

                    lblActualLastCheckin.Text = status.TimeTaken.ToString() + " (UTC)";
                    lblBatteryStatActual.Text = (status.BatteryPercentage * 100.0d).ToString() + "%";
                    lblBatteryTempActual.Text = status.BatteryTemp + "°C";
                    lblIsPluggedInActual.Text = status.IsCharging ? "Yes" : "No";
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}