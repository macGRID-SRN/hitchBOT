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
    public partial class Images : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    var imgs = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved == null && i.TimeDenied == null) && DbFunctions.DiffDays(i.TimeTaken, DateTime.UtcNow) <= 500).OrderBy(i => i.TimeTaken).ToList();

                    if (imgs.Count == 0)
                        Label1.Text = "No recent images! Uh oh!";
                    TableRow tr = new TableRow();
                    for (int i = 0; i < imgs.Count; i++)
                    {
                        var img = imgs[i];
                        Table innerTable = new Table();
                        TableRow upperRow = new TableRow();
                        TableRow imageRow = new TableRow();

                        TableCell upper = new TableCell();

                        Image newImage = new Image();
                        newImage.ImageUrl = "http://imgur.com/" + img.url + ".jpg";
                        newImage.Width = 400;
                        newImage.ImageAlign = ImageAlign.Right;
                        newImage.Style.Add("transform", "rotate(90deg)");
                        newImage.Style.Add("padding-top", "100px");
                        TableCell imageCell = new TableCell();
                        imageCell.Controls.Add(newImage);
                        imageRow.Cells.Add(imageCell);


                        Button myButton2 = new Button();
                        myButton2.CommandArgument = img.ID.ToString();
                        myButton2.Text = "Save";
                        myButton2.CssClass = "save_btn";
                        myButton2.Click += this.Button_Save_Image;
                        upper.Controls.Add(myButton2);
                        Button myButton1 = new Button();
                        myButton1.CommandArgument = img.ID.ToString();
                        myButton1.Text = "Remove";
                        myButton1.CssClass = "remove_btn";
                        myButton1.Click += this.Button_Remove_Image;
                        upper.Controls.Add(myButton1);


                        Label myLabel = new Label();
                        myLabel.Text = " <br>  Time Taken: " + img.TimeTaken.ToString() + " (UTC)";
                        upper.Controls.Add(myLabel);

                        upperRow.Controls.Add(upper);
                        upperRow.VerticalAlign = VerticalAlign.Top;
                        innerTable.Rows.Add(imageRow);
                        innerTable.Rows.Add(upperRow);

                        TableCell tc1 = new TableCell();
                        tc1.Controls.Add(innerTable);
                        tc1.VerticalAlign = VerticalAlign.Top;
                        tc1.Height = newImage.Width;
                        tc1.CssClass = "table_block";
                        tr.Cells.Add(tc1);

                        if (tr.Cells.Count >= 2)
                        {
                            tableViewImage.Rows.Add(tr);
                            tr = new TableRow();
                        }
                    }
                    if (tr.Cells.Count == 1)
                    {
                        tableViewImage.Rows.Add(tr);
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

            Response.Redirect("Images.aspx", true);
        }

        private void Button_Save_Image(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;
            using (var db = new Models.Database())
            {
                int ID = int.Parse(b.CommandArgument);
                var img = db.Images.First(i => i.ID == ID);

                img.TimeApproved = DateTime.UtcNow;

                db.Images.Attach(img);

                db.ChangeTracker.DetectChanges();

                db.SaveChanges();
            }

            Response.Redirect("Images.aspx", true);
        }
    }
}