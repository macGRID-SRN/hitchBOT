using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Map
{
    public partial class live : Page
    {
        int projectID;
        int hitchbotID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["hbID"], out projectID))
            {
                hitchbotID = int.Parse(Request.QueryString["hbID"]);

                jsDataLocation.Text = @"<script type=""text/javascript"" src=""http://hbsecure.blob.core.windows.net/hbjs/testLocations" + hitchbotID + @".js""></script>";

                gmapsString.Text = string.Format(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?key=AIzaSyCV-d9jbUEWesRS6LRsWCWZpKZdOmXCUWA""></script>");
            }
            else
            {
                throw new HttpException(404, "hitchBOT not found!!");
            }
        }
    }
}