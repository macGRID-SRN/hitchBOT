using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}