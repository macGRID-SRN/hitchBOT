using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class AddTargetLocation : System.Web.UI.Page
    {
        private int HitchBotID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    HitchBotID = user.hitchBOT.ID;
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}