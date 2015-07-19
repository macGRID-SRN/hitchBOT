using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hitchbot_secure_api.Dal;
using hitchbot_secure_api.Models;
using Microsoft.Ajax.Utilities;

namespace hitchbot_secure_api.Access
{
    public partial class ViewStats : System.Web.UI.Page
    {
        public string hitchBOTCharging = string.Empty;
        public string hitchBOTSpotUpdate = string.Empty;
        public string hitchBOTConversation = string.Empty;
        public string hitchBOTBatteryLevel = string.Empty;
        public string hitchBOTBatteryTemp = string.Empty;

        public string mostRecentUpdate
        {
            get
            {
                var mostRecent = updateTimeList.OrderByDescending(l => l).FirstOrDefault();

                return mostRecent.GetDelayFromNow();
            }
        }

        private List<DateTime?> updateTimeList = new List<DateTime?>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                int hitchBotId = (int)Session[SessionInfo.HitchBotId];

                using (var db = new DatabaseContext())
                {
                    var tabletStatus =
                        db.TabletStatuses.Where(l => l.HitchBotId == hitchBotId)
                            .OrderByDescending(l => l.TimeAdded)
                            .FirstOrDefault();

                    hitchBOTCharging = tabletStatus.IfNotNull(l => l.IsCharging ? "Yes" : "No", "Unknown");
                    tabletStatus.IfNotNull(l => updateTimeList.Add(l.TimeAdded));

                    hitchBOTBatteryLevel = tabletStatus.IfNotNull(l => (l.BatteryPercentage * 100).ToString("F") + "%", "Unknown");

                    hitchBOTBatteryTemp = tabletStatus.IfNotNull(l => l.BatteryTemp.ToString("F") + " degrees C", "Unknown");

                    var spotLocation =
                        db.Locations.Where(
                            l => l.HitchBotId == hitchBotId && l.LocationProvider == LocationProvider.SpotGPS)
                            .OrderByDescending(l => l.TimeAdded)
                            .FirstOrDefault();

                    hitchBOTSpotUpdate = spotLocation.IfNotNull(l => l.TimeAdded.GetDelayFromNow(), "Unknown");
                    //spotLocation.IfNotNull(l => updateTimeList.Add(l.TimeAdded));

                    var lastSpeechLog =
                        db.SpeechLogEvents.Where(l => l.HitchBotId == hitchBotId)
                            .OrderByDescending(l => l.TimeAdded)
                            .FirstOrDefault();

                    lastSpeechLog.IfNotNull(l => updateTimeList.Add(l.TimeAdded));

                    db.Images.Where(l => l.HitchBotId == hitchBotId)
                            .OrderByDescending(l => l.TimeAdded)
                            .FirstOrDefault()
                            .IfNotNull(l => updateTimeList.Add(l.TimeAdded));

                    db.Locations.Where(
                        l => l.HitchBotId == hitchBotId && l.LocationProvider == LocationProvider.TabletAGPS)
                        .OrderByDescending(l => l.TimeAdded)
                        .FirstOrDefault()
                        .IfNotNull(l => updateTimeList.Add(l.TimeAdded));
                }
            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }
    }
}