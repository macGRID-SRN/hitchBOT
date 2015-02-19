using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class LandingPage : System.Web.UI.Page
    {
        public static DateTime Map_Generation_Time = DateTime.UtcNow;

        const int DEFAULT_PROJECT_ID = 0;
        int projectID = DEFAULT_PROJECT_ID;

        const int DEFAILT_HITCHBOT_ID = 1;
        int hitchbotID = DEFAILT_HITCHBOT_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    this.hitchbotID = user.hitchBOT.ID;

                    //if there is a project available, display the info for that one instead.
                    if (user.Projects.FirstOrDefault() != null)
                        this.projectID = user.Projects.First().ID;

                    var lastLocationTime = db.Locations.Where(l => l.HitchBOT.ID == this.hitchbotID)
                        .OrderByDescending(l => l.TakenTime)
                        .First().TakenTime;

                    if (!IsPostBack)
                    {
                        LocationLBL.Text += lastLocationTime.ToString() + " (UTC)";
                        MapTimeLBL.Text += Map_Generation_Time.ToString() + " (UTC)";
                    }
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}