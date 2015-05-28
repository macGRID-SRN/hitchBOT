﻿using System;
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
        public async Task<IHttpActionResult> GetCleverscriptContext(int? HitchBotId)
        {
            if (!HitchBotId.HasValue)
            {
                var result = new
                {
                    data = new List<VariableValuePair>
                    {
                        new VariableValuePair
                        {
                            key = "current_city_name",
                            value = "Hamilton"
                        }
                    }
                };

                return Ok(result);
            }

            using (var db = new DatabaseContext())
            {

                var packets =
                    db.ContextPackets.Where(l => l.HitchBotId == HitchBotId)
                        .OrderByDescending(l => l.TimeCreated)
                        .FirstOrDefault();

                if (packets == null)
                {
                    BadRequest(string.Format("No cleverscript variables found for hitchBOT with ID: {0}.", HitchBotId));
                }

                var variables = db.VariableValuePairs.Where(l => l.ContextPacketId == packets.Id);

                if (!variables.Any())
                    BadRequest(string.Format("No cleverscript variables found for hitchBOT with ID: {0}.", HitchBotId));

                var result = new
                {
                    data = variables.ToList()
                };

                return Ok(result);
            }
        }
    }
}
