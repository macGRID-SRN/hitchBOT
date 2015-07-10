using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Access
{
    public partial class imageGrid : System.Web.UI.MasterPage
    {
        private List<Models.Image> _images;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                int hitchBotId = (int)Session[SessionInfo.HitchBotId];
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void SetImages(List<Models.Image> images)
        {
            _images = images;
        }

        protected List<Models.Image> GetImages()
        {
            return _images;
        }
    }
}