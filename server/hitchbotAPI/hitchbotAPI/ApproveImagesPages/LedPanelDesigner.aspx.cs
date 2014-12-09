using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class LedPanelDesigner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //verifies that there is someone logged in, kicks them out otherwise.
            if (Session["New"] != null)
            {
                //gets the current user for whatever use is needed.
                var user = (Models.Password)Session["New"];

                //this is how you would get the panels already saved.. watch out for lazy loading. 
                //user.Faces;
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (nullCheckOK())
            {
                string ext = System.IO.Path.GetExtension(fileUploadImage.FileName);

                string[] allowedExtenstions = new string[] { ".png", ".jpg", ".jpeg" };

                if (allowedExtenstions.Contains(ext))
                {
                    Bitmap bitmap = new Bitmap(fileUploadImage.PostedFile.InputStream);
                    Helpers.PanelHelper panelHelper = new Helpers.PanelHelper(bitmap, txtImageName.Text, txtImageDescription.Text, (Models.Password)Session["New"]);
                    Controllers.LedPanelController.addFace(panelHelper.getFace());
                    Response.Redirect("LedPanelPreview.aspx");
                }
                else
                {
                    lblWarning.Text = "Wrong Image format!";
                    lblWarning.ForeColor = Color.Red;
                }
            }
            else
            {
                lblWarning.Text = "Need at least an image and name to proceed.";
                lblWarning.ForeColor = Color.Red;
            }
        }

        private bool nullCheckOK()
        {
            return fileUploadImage.HasFile && txtImageName.Text != "";
        }
    }
}