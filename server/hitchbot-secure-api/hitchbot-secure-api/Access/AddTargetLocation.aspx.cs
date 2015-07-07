using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Access
{
    public partial class AddTargetLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                int hitchBotId = (int)Session[SessionInfo.HitchBotId];

                using (var db = new Dal.DatabaseContext())
                {
                    var labels = db.CleverscriptContexts.Where(l => l.HitchBotId == hitchBotId).Select(l => new
                    {
                        l.Id,
                        l.HumanReadableBaseLabel
                    }).AsEnumerable();

                    labelLiterals.Text += string.Join("", labels.Select(l => string.Format(@"<li><a class=""fake-link label-option"" value=""{1}"">{0}</a></li>", l.HumanReadableBaseLabel, l.Id)));
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var wikiEntry = inputWiki1.InnerText;
                var radius = inputRadiusValue.Value;
                var name = inputName.Value;
                var lat = inputLat.Value;
                var lng = inputLong.Value;

                double? radiusActual = null;
                double latActual;
                double lngActual;

                using (var db = new Dal.DatabaseContext())
                {
                    var user = (Models.LoginAccount)Session["New"];
                    var hitchbotId = user.HitchBotId;

                    Models.Location location = null;

                    var contextID = int.Parse(selectedLabelID.Value);
                    var context = db.CleverscriptContexts.FirstOrDefault(l => l.Id == contextID);

                    if (context == null)
                    {
                        setErrorMessage("Error with the cleverscript label!!");
                        return;
                    }

                    if (LocationCheckBox.Checked)
                    {
                        /*
                        nullable double parse code borrowed from
                        http://stackoverflow.com/questions/3390750/how-to-use-int-tryparse-with-nullable-int
                        */
                        double tmp;

                        if (!double.TryParse(radius, out tmp))
                        {
                            setErrorMessage("Selected Radius is not valid!");
                            return;
                        }
                        radiusActual = tmp;

                        if (!double.TryParse(lat, out latActual))
                        {
                            setErrorMessage("Latitude is not valid number!");
                            return;
                        }

                        if (!double.TryParse(lng, out lngActual))
                        {
                            setErrorMessage("Longitude is not valid number!");
                            return;
                        }

                        location = new Models.Location
                        {
                            Latitude = latActual,
                            Longitude = lngActual,
                            TimeAdded = DateTime.UtcNow,
                            TakenTime = DateTime.UtcNow
                        };

                        db.Locations.Add(location);
                        db.SaveChanges();
                    }

                    var wiki = new Models.CleverscriptContent
                    {
                        LocationId = location.Id,
                        CleverText = wikiEntry,
                        EntryName = name,
                        RadiusKm = radiusActual,
                        HitchBotId = hitchbotId,
                        TimeAdded = DateTime.UtcNow,
                        CleverscriptContextId = context.Id,
                        isBucketList = bucketCheckBox.Checked
                    };

                    db.CleverscriptContents.Add(wiki);

                    db.SaveChanges();
                }

                Response.Redirect("AddTargetSuccess.aspx");
            }
            catch
            {
                setErrorMessage("An unknown error occurred. let the sys admin know you saw this message.");
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