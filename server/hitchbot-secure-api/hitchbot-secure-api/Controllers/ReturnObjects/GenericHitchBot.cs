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

        public int? TimeTakenUnix { get; set; }

        public string TimeTaken { get; set; }

        public DateTime TakenTime
        {
            get
            {
                if (TimeTakenUnix.HasValue)
                {
                    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)TimeTakenUnix);
                }

                if (!string.IsNullOrWhiteSpace(TimeTaken))
                {
                    return DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                }

                return DateTime.UtcNow;
            }
        }
    }
}
