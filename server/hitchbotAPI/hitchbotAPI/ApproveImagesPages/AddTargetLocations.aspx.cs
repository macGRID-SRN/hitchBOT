using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class AddTargetLocations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            var wikiEntry = inputWiki1.InnerText;
            var radius = inputRadiusValue.Value;
            var name = inputName.Value;
            var lat = inputLat.Value;
            var lng = inputLong.Value;

            var user = (Models.Password)Session["New"];

            using (var db = new Models.Database())
            {
                var hitchbot = user.hitchBOT;
            }
        }

        protected void setErrorMessage(string error)
        {
            this.errorAlert.Attributes.Remove("class");
            this.errorAlert.Attributes.Add("class", "alert alert-danger");
            this.errorAlert.InnerText = error;
        }
    }
}