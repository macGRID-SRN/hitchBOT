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

        string imgTagOff = "../Images/ledoff.png";
        string imgTagOn = "../Images/ledon.png";
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
            ImageButton b = sender as ImageButton;
            if (b.ImageUrl.Equals(imgTagOff))
            {

                b.ImageUrl = imgTagOn;
            }
            else
            {

                b.ImageUrl = imgTagOff;
            }
        }

        private void loopThroughLEDs()
        {
            List<bool> tempList = new List<bool>();
            List<byte> byteList = new List<byte>();

            foreach(TableRow row in Table1.Rows)
            {
                foreach(TableCell cell in row.Cells)
                {
                    ImageButton[] iBArray = new ImageButton[1];
                    cell.Controls.CopyTo(iBArray,0);
                    int rowIndex = int.Parse(((ImageButton)iBArray.GetValue(0)).CommandArgument.Split(',')[0]);
                    int columnIndex = int.Parse(((ImageButton)iBArray.GetValue(0)).CommandArgument.Split(',')[1]);
                    if (((ImageButton)iBArray.GetValue(0)).ImageUrl.Equals(imgTagOff))
                    {
                        tempList.Add(false);
                    }
                    else
                    {
                        tempList.Add(true);
                    }
                    if(tempList.Count == 8)
                    {
                        byteList.Add(Helpers.PanelHelper.convertToByte(tempList.ToArray()));
                        tempList = new List<bool>();
                    }
                }
            }
            Helpers.PanelHelper pHelper = new Helpers.PanelHelper(byteList, face.Name, face.Description, face.UserAccount);
            Models.Face tempFace = pHelper.getFace();

            //I'm making this object here because otherwise a duplicate is added to the db due to the ID from the other face
            Models.Face faceToInsert = new Models.Face
            {
                Name = face.Name,
                Description = tempFace.Description,
                TimeAdded = tempFace.TimeAdded,
                Approved = tempFace.Approved,
                Panels = tempFace.Panels,
                UserAccount = tempFace.UserAccount
            };
            Controllers.LedPanelController.addFace(faceToInsert);
        }


        private void makePanel()
        {
            Models.LedPanel panel = face.Panels.ToList<Models.LedPanel>()[0];
            List<Models.Row> rows = panel.Rows.ToList<Models.Row>();
            bool[] bits;

            for (int i = 0; i < 16; i++)
            {
                TableRow tRow1 = new TableRow();
                for (int j = 0; j < 24; j++)
                {
                    ImageButton iButton1 = new ImageButton();
                    switch(j / 8)
                    {
                        case 0:
                            bits = Helpers.PanelHelper.GetBits(rows[i].ColSet0).ToArray();
                            break;
                        case 1:
                            bits = Helpers.PanelHelper.GetBits(rows[i].ColSet1).ToArray();
                            break;
                        case 2:
                            bits = Helpers.PanelHelper.GetBits(rows[i].ColSet2).ToArray();
                            break;
                        default:
                            bits = Helpers.PanelHelper.GetBits(rows[i].ColSet0).ToArray();
                            break;
                    }
                    int l = bits.Length;
                    if ((bool)bits.GetValue(j - (j / 8) * 8))
                    {
                        iButton1.ImageUrl = imgTagOn;

                    }
                    else
                    {
                        iButton1.ImageUrl = imgTagOff;
                    }
                    iButton1.Height = 20;
                    iButton1.Width = 20;
                    iButton1.CommandArgument = i.ToString() + "," + j.ToString();
                    iButton1.Click += RowClick1;

                    TableCell tCell1 = new TableCell();
                    tCell1.Controls.Add(iButton1);
                    tRow1.Cells.Add(tCell1);
                }
                Table1.Rows.Add(tRow1);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            loopThroughLEDs();
            Response.Redirect("LedPanelDesigner.aspx");
        }
    }
}