using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using hitchbot_secure_api.Dal;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public class HitchBotController : ApiController
    {
        [HttpGet]
        public string Test(string repeat)
        {

            return repeat;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCleverscriptContext()
        {
            using (var db = new DatabaseContext())
            {
                var result = new ContextPacket
                {
                    data = new List<KeyValuePair>
                    {
                        new KeyValuePair
                        {
                            key = "current_city_name",
                            value = "Hamilton"
                        }
                    }
                };

                return Ok(result);
            }
        }
    }
}
