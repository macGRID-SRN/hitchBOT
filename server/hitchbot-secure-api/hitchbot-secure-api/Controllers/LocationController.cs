using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class LocationController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> UpdateLocation([FromBody] ReturnLocation Context)
        {
            using (var db = new Dal.DatabaseContext())
            {
                db.Locations.Add(
                    new Location(Context)
                {
                    Latitude = Context.Latitude,
                    Longitude = Context.Longitude,
                    Altitude = Context.Altitude,
                    Accuracy = Context.Accuracy,
                    Velocity = Context.Velocity,
                    LocationProvider = Context._LocationProvider,
                });

                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
