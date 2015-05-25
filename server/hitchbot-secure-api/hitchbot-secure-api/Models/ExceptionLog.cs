using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Arguments { get; set; }
        public string Method { get; set; }
        public string Data { get; set; }
        public string Action { get; set; }

        public DateTime TimeOccured { get; set; }
        public DateTime TimeAdded { get; set; }
        
        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public ExceptionLog()
        {
            TimeAdded = DateTime.UtcNow;
        }

        public ExceptionLog(Controllers.ReturnObjects.GenericHitchBot context)
            : this()
        {
            TimeOccured = context.DateTime;
            HitchBotId = context.HitchBotId;
        }
    }
}
