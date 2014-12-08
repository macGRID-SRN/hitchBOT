using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class LedPanelPreview : System.Web.UI.Page
    {

        string imgTagOff = "Images/ledoff.png";
        string imgTagOn = "Images/ledon.png";
        Models.Face face;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                this.face = Controllers.LedPanelController.getLastFace(user);
                makePanel();
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
            
        }

        protected void RowClick1(object sender, EventArgs e)
        {
            //The below code is to be implemented in the future to allow users to modify the panels.
            /*
            ImageButton b = sender as ImageButton;
            int rowIndex = int.Parse(b.CommandArgument.Split(',')[0]);
            int columnIndex = int.Parse(b.CommandArgument.Split(',')[1]);
            int linearElement = (columnIndex - 1) * 24 + rowIndex;
            if (b.ImageUrl.Equals(imgTagOff))
            {
                b.ImageUrl = imgTagOn;
                face.Panels.Rows[columnIndex]
                //TODO modifiy face
            }
            else
            {
                b.ImageUrl = imgTagOff;
                //TODO Modifiy face
            }
           */
        }

        private void makePanel()
        {
            for (int i = 0; i < 16; i++)
            {
                TableRow tRow1 = new TableRow();

                for (int j = 0; j < 24; j++)
                {
                    ImageButton iButton1 = new ImageButton();
                    iButton1.ImageUrl = imgTagOff;
                    iButton1.Height = 30;
                    iButton1.Width = 30;
                    iButton1.CommandArgument = i.ToString() + "," + j.ToString();
                    iButton1.Click += RowClick1;

                    TableCell tCell1 = new TableCell();
                    tCell1.Controls.Add(iButton1);
                    tRow1.Cells.Add(tCell1);
                }
                Table1.Rows.Add(tRow1);


            }
        }
    }
}