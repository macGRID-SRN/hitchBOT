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
        const int DEFAULT_PROJECT_ID = 1;
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

                    this.DynamicMapsTestButton.NavigateUrl = "DynamicMap.aspx?prj=" + this.projectID;
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void TextJsButton_Click(object sender, EventArgs e)
        {
            Helpers.Location.GoogleMapsHelper.BuildLocationJS(hitchbotID);
            string TargetLocation = Helpers.PathHelper.GetJsBuildPath();
            Helpers.AzureBlobHelper.UploadLocationJsAndGetPublicUrl(TargetLocation, Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME);
        }
    }
}