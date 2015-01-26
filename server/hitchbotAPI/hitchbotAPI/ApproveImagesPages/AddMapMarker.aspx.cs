using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class AddMapMarker : System.Web.UI.Page
    {
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

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var popHeaderEnglish = inputHeaderEnglish.Value;
                var popHeaderGerman = inputHeaderGerman.Value;
                var popContentEnglish = inputPopupContentEnglish.InnerText;
                var popContentGerman = inputPopupContentGerman.InnerText;

                var radius = inputRadiusValue.Value;
                var lat = inputLat.Value;
                var lng = inputLong.Value;

                double radiusActual;
                double latActual;
                double lngActual;

                using (var db = new Models.Database())
                {
                    var user = (Models.Password)Session["New"];
                    var projID = user.Projects.First().ID;
                    var project = db.Projects.First(l => l.ID == projID);

                    Models.Location location = null;

                    if (!double.TryParse(radius, out radiusActual))
                    {
                        setErrorMessage("Selected Radius is not valid!");
                        return;
                    }

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

                    var wiki = new Models.MapMarker()
                    {
                        TargetLocation = location,
                        RadiusKM = radiusActual,
                        TimeAdded = DateTime.UtcNow,
                        Project = project,
                        Active = true,
                        HasBeenVisited = false,
                        BodyText = popContentEnglish,
                        BodyTextGerman = popContentGerman,
                        HeaderText = popHeaderEnglish,
                        HeaderTextGerman = popHeaderGerman
                    };

                    db.MapMarkers.Add(wiki);

                    db.SaveChanges();
                }

                Response.Redirect("AddMapMarkerSuccess.aspx");
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