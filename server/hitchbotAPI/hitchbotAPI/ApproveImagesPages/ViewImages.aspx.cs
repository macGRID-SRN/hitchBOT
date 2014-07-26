using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Data.Objects.SqlClient;
using System.Globalization;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class ViewImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    var imgs = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved == null || i.TimeDenied == null) && DbFunctions.DiffDays( i.TimeTaken, DateTime.UtcNow) <= 1).OrderBy(i => i.TimeTaken).ToList();
                    foreach (var img in imgs)
                    {
                        Image newImage = new Image();
                        newImage.ImageUrl = "http://imgur.com/" + img.url + ".jpg";
                        newImage.Width = 700;
                        newImage.Style.Add("transform", "rotate(90deg)");
                        TableRow tr = new TableRow();
                        TableCell tc = new TableCell();
                        tc.Controls.Add(newImage);
                        tc.Height = newImage.Width;
                        tr.Cells.Add(tc);
                        tableViewImage.Rows.Add(tr);
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