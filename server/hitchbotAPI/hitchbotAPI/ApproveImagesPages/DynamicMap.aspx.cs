using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Globalization;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class DynamicMap : System.Web.UI.Page
    {
        int projectID;
        int hitchbotID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["hbID"], out this.projectID))
            {
                this.hitchbotID = int.Parse(Request.QueryString["hbID"]);


                var langOptions = string.Empty;

                if (!string.IsNullOrEmpty(Request.QueryString["lang"]))
                {
                    Page.UICulture = Request.QueryString["lang"];
                    Culture = Request.QueryString["lang"];
                    HtmlMasterTag.Attributes["lang"] = Request.QueryString["lang"];
                    langOptions = "&language=" + Request.QueryString["lang"] + "&";
                }

                this.jsDataLocation.Text = @"<script type=""text/javascript"" src=""http://hitchbotimg.blob.core.windows.net/hbjs/testLocations" + Request.QueryString["lang"] + hitchbotID + @".js""></script>";

                this.gmapsString.Text = string.Format(@"<script type=""text/javascript"" src=""https://maps.googleapis.com/maps/api/js?{0}key=AIzaSyCV-d9jbUEWesRS6LRsWCWZpKZdOmXCUWA""></script>", langOptions);
            }
            else
            {
                throw new HttpException(500, "Invalid project code!");
            }
        }
    }
}