using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class DynamicMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string builtJavaScriptPoints = string.Empty;
            using (var db = new Models.Database())
            {
                var locations = db.hitchBOTs.Include(l => l.Locations).SingleOrDefault(l => l.ID == 3).Locations;

                locations = Helpers.LocationHelper.SlimLocations(locations);


            }
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "aKey", "alert('this worked');");
        }
    }
}