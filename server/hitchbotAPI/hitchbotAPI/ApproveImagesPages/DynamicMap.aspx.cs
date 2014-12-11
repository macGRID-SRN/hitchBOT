using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class DynamicMap : System.Web.UI.Page
    {
        int projectID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["prj"], out this.projectID))
            {
                this.projectID = int.Parse(Request.QueryString["prj"]);

                this.jsDataLocation.Text = @"<script type=""text/javascript"" src=""http://hitchbotimg.blob.core.windows.net/hbjs/testLocations.js""></script>";
            }
            else
            {
                throw new HttpException(500, "Invalid project code!");
            }
        }
    }
}