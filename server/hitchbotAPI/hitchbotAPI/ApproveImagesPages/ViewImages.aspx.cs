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
                    var imgs = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved == null || i.TimeDenied == null) && DbFunctions.DiffDays(i.TimeTaken, DateTime.UtcNow) <= 1).OrderBy(i => i.TimeTaken).ToList();
                    for (int i = 0; i <= imgs.Count; i += 2)
                    {
                        var img1 = imgs[i];
                        var img2 = i < imgs.Count -1 ? imgs[i + 1] : null;

                        Image newImage = new Image();
                        newImage.ImageUrl = "http://imgur.com/" + img1.url + ".jpg";
                        newImage.Width = 700;
                        newImage.Style.Add("transform", "rotate(90deg)");

                        TableRow tr = new TableRow();
                        TableCell tc1 = new TableCell();


                        tc1.Controls.Add(newImage);
                        tc1.Height = newImage.Width;
                        tr.Cells.Add(tc1);

                        if (img2 != null)
                        {
                            TableCell tc2 = new TableCell();
                            Image newImage2 = new Image();
                            newImage2.ImageUrl = "http://imgur.com/" + img2.url + ".jpg";
                            newImage2.Width = 700;
                            newImage2.Style.Add("transform", "rotate(90deg)");
                            tc2.Controls.Add(newImage2);
                            tc2.Height = newImage2.Height;
                            tr.Cells.Add(tc2);
                        }
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