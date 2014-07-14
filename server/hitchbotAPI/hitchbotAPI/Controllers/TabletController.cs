using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Data.Entity;

namespace hitchbotAPI.Controllers
{
    public class TabletController : ApiController
    {
        [HttpPost]
        public bool UpdateTabletStatus(int HitchBotID, string timeTaken, bool isPluggedIn, double BatteryVoltage, double BatteryPercent, double BatteryTemp)
        {
            DateTime TimeTaken = DateTime.ParseExact(timeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            using (var db = new Models.Database())
            {
                var hitchbot = db.hitchBOTs.First(h => h.ID == HitchBotID);

                var TabletStatus = new Models.TabletStatus()
                {
                    TimeAdded = DateTime.UtcNow,
                    TimeTaken = TimeTaken,
                    BatteryTemp = BatteryTemp,
                    BatteryPercentage = BatteryPercent,
                    BatteryVoltage = BatteryVoltage,
                    IsCharging = isPluggedIn,
                    HitchBOT = hitchbot

                };

                db.TabletStatusI.Add(TabletStatus);
                db.SaveChanges();
            }

            return true;
        }
    }
}