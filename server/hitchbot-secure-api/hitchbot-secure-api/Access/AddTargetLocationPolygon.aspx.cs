using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Access
{
    public partial class AddTargetLocationPolygon : System.Web.UI.Page
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
                var name = inputName.Value;
                int? contextIdNullable = null;

                var locations = locationArray.Value.Split(',').Select(l =>
                {
                    var coord = l.Split(':');
                    return new Models.Location
                    {
                        Latitude = double.Parse(coord[0]),
                        Longitude = double.Parse(coord[1])
                    };
                }).ToList();

                using (var db = new Dal.DatabaseContext())
                {
                    var user = (Models.LoginAccount)Session["New"];
                    var hitchbotId = user.HitchBotId;

                    int contextID;

                    if (!bucketCheckBox.Checked)
                    {
                        if (!int.TryParse(selectedLabelID.Value, out contextID))
                        {
                            setErrorMessage("Error with the cleverscript label!!");
                            return;
                        }
                        contextIdNullable = contextID;
                    }

                    if (locations.Count < 3)
                    {
                        setErrorMessage("More than 2 locations must be selected.");
                        return;
                    }

                    locations.ForEach(l =>
                    {
                        db.Locations.Add(l);
                    });

                    var wiki = new Models.CleverscriptContent
                    {
                        CleverText = wikiEntry,
                        EntryName = name,
                        HitchBotId = hitchbotId,
                        CleverscriptContextId = contextIdNullable,
                        TimeAdded = DateTime.UtcNow,
                        isBucketList = bucketCheckBox.Checked
                    };

                    db.CleverscriptContents.Add(wiki);

                    db.SaveChanges();

                    locations.ForEach(l =>
                    {
                        db.PolgonVertices.Add(new Models.PolgonVertex
                        {
                            LocationId = l.Id,
                            CleverscriptContentId = wiki.Id
                        });
                    });


                    db.SaveChanges();
                }

                Response.Redirect("AddTargetSuccess.aspx");
            }
            catch (Exception ex)
            {
                setErrorMessage("An unknown error occurred. let the sys admin know you saw this message.");
            }
        }


        protected void setErrorMessage(string error)
        {
            error += " Please refresh the page and try again.";
            this.errorAlert.Attributes.Remove("class");
            this.errorAlert.Attributes.Add("class", "alert alert-danger");
            this.errorAlert.InnerText = error;
        }
    }

    //taken from http://stackoverflow.com/questions/521687/c-sharp-foreach-with-index
    //TODO: move this class to its own file.
    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

    }
}