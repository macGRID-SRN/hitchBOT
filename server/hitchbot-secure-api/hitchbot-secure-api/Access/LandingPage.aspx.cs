using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Access
{
    public partial class LandingPage : System.Web.UI.Page
    {
        private int hitchBotId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {

            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}