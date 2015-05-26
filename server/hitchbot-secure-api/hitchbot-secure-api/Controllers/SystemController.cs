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
    }
}
