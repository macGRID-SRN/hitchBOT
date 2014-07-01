using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace hitchbotAPI
{
    public partial class ViewImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    var img = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved == null || i.TimeDenied == null)).OrderBy(i => i.TimeTaken);
                    this.imagePreview.ImageUrl = "http://imgur.com/" + img.FirstOrDefault().url + ".jpg";
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void Approve_Click(object sender, EventArgs e)
        {

        }

        protected void Deny_Click(object sender, EventArgs e)
        {

        }
    }
}