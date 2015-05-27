using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Controllers.ReturnObjects
{
    public class GenericHitchBot
    {
        public int HitchBotId { get; set; }

        public int? TimeUnix { get; set; }

        public string Time { get; set; }

        public DateTime DateTime
        {
            get
            {
                if (TimeUnix.HasValue)
                {
                    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(((double)TimeUnix)/1000);
                }

                if (!string.IsNullOrWhiteSpace(Time))
                {
                    return DateTime.ParseExact(Time, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                }

                return DateTime.UtcNow;
            }
        }
    }
}
