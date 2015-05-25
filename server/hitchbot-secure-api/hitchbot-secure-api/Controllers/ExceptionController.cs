using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Management;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class ExceptionController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> AddException([FromBody] ReturnException context)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model sent was not valid.");

            using (var db = new Dal.DatabaseContext())
            {
                db.ExceptionLogs.Add(new ExceptionLog(context)
                {
                    Action = context.Action,
                    Arguments = context.Arguments,
                    Data = context.Data,
                    Exception = context.Exception,
                    Message = context.Message,
                    Method = context.Method,
                });

                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
