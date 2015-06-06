using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace hitchbot_secure_api.Controllers
{
    public partial class SystemController : ApiController
    {
        public static DateTime MAP_LAST_UPDATED = DateTime.UtcNow;
        public async Task<IHttpActionResult> TestModel([FromBody] ReturnTest Context)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state is not valid");

            return Ok();
        }

        public async Task<IHttpActionResult> TestDatabase()
        {
            using (var db = new Dal.DatabaseContext())
            {
                return Ok(db.Database.Exists());
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> BuildJs(int hitchBotId)
        {
            var builer = new Helpers.Location.GoogleMapsBuilder(hitchBotId);
            builer.BuildJsAndUpload();
            //HALLELIHA THE MAP WAS UPDATED
            MAP_LAST_UPDATED = DateTime.UtcNow;

            //Also basically this.
            //TIME_SINCE_LAST_MAP_UPDATE_EMAIL { get { return DateTime.UtcNow; } }
            return Ok(string.Format("Map was successfully updated."));
        }
    }
}
