using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hitchbot_secure_api.Dal;

namespace hitchbot_secure_api.Access
{
    public partial class ViewSavedImages : System.Web.UI.Page
    {
        public int currentSkip = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                int hitchBotId = (int)Session[SessionInfo.HitchBotId];

                var skip = Request.QueryString["skip"];
                int skipOver = 0;

                if (int.TryParse(skip, out skipOver))
                {
                    currentSkip = skipOver;
                }

                using (var db = new DatabaseContext())
                {
                    var imageList = db.Images.Where(l => !l.TimeDenied.HasValue && l.TimeApproved.HasValue && l.HitchBotId == hitchBotId).OrderByDescending(l => l.TimeTaken).Skip(skipOver).Take(50).ToList();
                    var master = Master as imageGrid;

                    master.SetImageSkip(currentSkip);
                    master.SetImages(imageList);
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}