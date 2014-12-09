using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.Collections;

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
            ImageButton b = sender as ImageButton;
            int rowIndex = int.Parse(b.CommandArgument.Split(',')[0]);
            int columnIndex = int.Parse(b.CommandArgument.Split(',')[1]);
            if (b.ImageUrl.Equals(imgTagOff))
            {
                face.Panels.ToList<Models.LedPanel>()[0].Rows = fixRow(rowIndex, columnIndex, true);
                Controllers.LedPanelController.updateFace(face, false);
                b.ImageUrl = imgTagOn;
            }
            else
            {
                face.Panels.ToList<Models.LedPanel>()[0].Rows = fixRow(rowIndex, columnIndex, false);
                Controllers.LedPanelController.updateFace(face, false);
                b.ImageUrl = imgTagOff;
            }

        }

        private List<Models.Row> fixRow(int row, int col, bool valueToChangeTo)
        {
            List<Models.Row> rows = face.Panels.ToList<Models.LedPanel>()[0].Rows.ToList<Models.Row>();
            BitArray bits;

            //Integer division done by purpose!
            switch (row / 8)
            {
                case 0:
                    bits = new BitArray(rows[col].ColSet0);
                    bits[row - (row / 8) * 8] = valueToChangeTo;
                    rows[col].ColSet0 = Helpers.PanelHelper.convertToByte(bits);
                    break;
                case 1:
                    bits = new BitArray(rows[col].ColSet1);
                    bits[row - (row / 8) * 8] = valueToChangeTo;
                    rows[col].ColSet1 = Helpers.PanelHelper.convertToByte(bits);
                    break;
                case 2:
                    bits = new BitArray(rows[col].ColSet2);
                    bits[row - (row / 8) * 8] = valueToChangeTo;
                    rows[col].ColSet2 = Helpers.PanelHelper.convertToByte(bits);
                    break;
            }

            return rows;
        }

        private void makePanel()
        {
            Models.LedPanel panel = face.Panels.ToList<Models.LedPanel>()[0];
            List<Models.Row> rows = panel.Rows.ToList<Models.Row>();
            BitArray bits;

            for (int i = 0; i < 16; i++)
            {
                TableRow tRow1 = new TableRow();
                for (int j = 0; j < 24; j++)
                {
                    ImageButton iButton1 = new ImageButton();
                    switch(i / 8)
                    {
                        case 0:
                            bits = new BitArray(rows[j].ColSet0);
                            break;
                        case 1:
                            bits = new BitArray(rows[j].ColSet1);
                            break;
                        case 2:
                            bits = new BitArray(rows[j].ColSet2);
                            break;
                        default:
                            bits = new BitArray(rows[j].ColSet2);
                            break;
                    }
                    if (bits[i - (i / 8) * 8])
                    {
                        iButton1.ImageUrl = imgTagOn;

                    }
                    else
                    {
                        iButton1.ImageUrl = imgTagOff;
                    }
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