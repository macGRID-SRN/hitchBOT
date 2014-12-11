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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    if (user.Projects.FirstOrDefault() != null)
                        this.DynamicMapsTestButton.NavigateUrl = "DynamicMap.aspx?prj=" + user.Projects.First().ID;
                    else
                        this.DynamicMapsTestButton.NavigateUrl = "DynamicMap.aspx?prj=1";
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void TextJsButton_Click(object sender, EventArgs e)
        {
            Helpers.LocationHelper.BuildLocationJS(3);
            string TargetLocation = Helpers.PathHelper.GetJsBuildPath();
            Helpers.AzureBlobHelper.UploadLocationJsAndGetPublicUrl(TargetLocation, Helpers.AzureBlobHelper.JS_LOCATION_FILE_NAME);
        }
    }
}