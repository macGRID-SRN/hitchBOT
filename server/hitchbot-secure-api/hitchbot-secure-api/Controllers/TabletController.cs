using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class TabletController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> UpdateTabletStatus([FromBody] ReturnTabletStatus context)
        {
            using (var db = new Dal.DatabaseContext())
            {
                db.TabletStatuses.Add(new TabletStatus(context)
                {
                    BatteryPercentage = context.BatteryPercentage,
                    BatteryTemp = context.BatteryTemp,
                    BatteryVoltage = context.BatteryVoltage,
                    IsCharging = context.IsCharging
                });

                await db.SaveChangesAsync();
            }

            return Ok();
        }
    }
}