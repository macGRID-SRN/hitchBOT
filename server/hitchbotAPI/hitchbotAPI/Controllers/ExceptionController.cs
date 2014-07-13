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
    public class ExceptionController : ApiController
    {
        [HttpPost]
        public bool AddException(int HitchBotID, string Message, string TimeOccured)
        {
            using (var db = new Models.Database())
            {
                DateTime RealTimeOccured = DateTime.ParseExact(TimeOccured, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var hitchBOT = db.hitchBOTs.First(h => h.ID == HitchBotID);

                var exception = new Models.ExceptionLog()
                {
                    Message = Message,
                    TimeOccured = RealTimeOccured,
                    HitchBOT = hitchBOT
                };

                db.Exceptions.Add(exception);
                db.SaveChanges();
            }
            return true;
        }
    }
}