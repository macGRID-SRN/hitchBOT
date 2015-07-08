using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbot_secure_api.Models
{
    public class TabletSerial
    {
        public int Id { get; set; }
        public string TabletSerialNumber { get; set; }
        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }
    }
}