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
        public async Task<IHttpActionResult> LogLocation([FromBody] ReturnLocation Context)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model sent was not valid.");

            using (var db = new Dal.DatabaseContext())
            {
                db.Locations.Add(
                    new Models.Location(Context)
                {
                    Latitude = Context.Latitude,
                    Longitude = Context.Longitude,
                    Altitude = Context.Altitude,
                    Accuracy = Context.Accuracy,
                    Velocity = Context.Velocity,
                    LocationProvider = Context._LocationProvider
                });

                await db.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet]
        public void FindGeoArea(int hitchBotId)
        {
        }
    }
}
