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
    public partial class SavedImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    int hitchBOTid = user.hitchBOT.ID;
                    var imgs = db.Images.Include(i => i.HitchBOT).Where(i => i.HitchBOT.ID == hitchBOTid && (i.TimeApproved != null && i.TimeDenied == null)).OrderBy(i => i.TimeTaken).ToList();

                    if (imgs.Count == 0)
                        Label1.Text = "There are no saved images!";

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
                        newImage.Width = 700;
                        newImage.ImageAlign = ImageAlign.Right;
                        newImage.Style.Add("transform", "rotate(90deg)");
                        newImage.Style.Add("margin-top", "25px");
                        TableCell imageCell = new TableCell();
                        imageCell.Controls.Add(newImage);
                        imageRow.Cells.Add(imageCell);

                        Label myLabel = new Label();
                        myLabel.Text = "Time Taken: " + img.TimeTaken.ToString() + " (UTC)" + "  Time Saved: " + img.TimeApproved.ToString() + " (UTC)";
                        upper.Controls.Add(myLabel);

                        upperRow.Controls.Add(upper);
                        upperRow.Height = 105;
                        upperRow.VerticalAlign = VerticalAlign.Top;
                        innerTable.Rows.Add(upperRow);
                        innerTable.Rows.Add(imageRow);

                        TableCell tc1 = new TableCell();
                        tc1.Controls.Add(innerTable);
                        tc1.VerticalAlign = VerticalAlign.Top;
                        tc1.Height = newImage.Width;
                        tc1.Height = Unit.Parse((newImage.Width.Value + 60).ToString(), System.Globalization.CultureInfo.CurrentCulture);
                        tc1.BorderStyle = BorderStyle.Solid;
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

        //private void Button_Remove_Image(object sender, System.EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    using (var db = new Models.Database())
        //    {
        //        int ID = int.Parse(b.CommandArgument);
        //        var img = db.Images.First(i => i.ID == ID);

        //        img.TimeDenied = DateTime.UtcNow;

        //        db.Images.Attach(img);

        //        db.ChangeTracker.DetectChanges();

        //        db.SaveChanges();
        //    }

        //    Response.Redirect("ViewImages.aspx", true);
        //}

        //private void Button_Save_Image(object sender, System.EventArgs e)
        //{
        //    Button b = (Button)sender;
        //    using (var db = new Models.Database())
        //    {
        //        int ID = int.Parse(b.CommandArgument);
        //        var img = db.Images.First(i => i.ID == ID);

        //        img.TimeApproved = DateTime.UtcNow;

        //        db.Images.Attach(img);

        //        db.ChangeTracker.DetectChanges();

        //        db.SaveChanges();
        //    }

        //    Response.Redirect("ViewImages.aspx", true);
        //}
    }
}