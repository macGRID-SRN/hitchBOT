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
                    var imgs = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved == null && i.TimeDenied == null) && DbFunctions.DiffDays(i.TimeTaken, DateTime.UtcNow) <= 1).OrderBy(i => i.TimeTaken).ToList();

                    TableRow tr = new TableRow();
                    for (int i = 0; i < imgs.Count; i++)
                    {
                        var img = imgs[i];

                        Image newImage = new Image();
                        newImage.ImageUrl = "http://imgur.com/" + img.url + ".jpg";
                        newImage.Width = 700;
                        newImage.Style.Add("transform", "rotate(90deg)");

                        TableCell tc1 = new TableCell();

                        Button myButton1 = new Button();
                        myButton1.CommandArgument = img.ID.ToString();
                        myButton1.Text = "Remove";
                        myButton1.Click += this.Button_Remove_Image;
                        tc1.Controls.Add(myButton1);

                        tc1.Controls.Add(newImage);
                        tc1.Height = newImage.Width;
                        tc1.Height = Unit.Parse((newImage.Width.Value + 25).ToString(), System.Globalization.CultureInfo.CurrentCulture);
                        tc1.BorderStyle = BorderStyle.Solid;
                        tr.Cells.Add(tc1);

                        if (tr.Cells.Count >= 2)
                        {
                            tableViewImage.Rows.Add(tr);
                            tr = new TableRow();
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        private void Button_Remove_Image(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            using (var db = new Models.Database())
            {
                int ID = int.Parse(b.CommandArgument);
                var img = db.Images.First(i => i.ID == ID);

                img.TimeDenied = DateTime.UtcNow;

                db.Images.Attach(img);

                db.ChangeTracker.DetectChanges();

                db.SaveChanges();
            }

            Response.Redirect("ViewImages.aspx", true);
        }
    }
}