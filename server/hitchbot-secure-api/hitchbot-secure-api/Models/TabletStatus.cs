using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class TabletStatus
    {
        public int ID { get; set; }
        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime TimeAdded { get; set; }
        public double BatteryTemp { get; set; }
        public double BatteryVoltage { get; set; }
        public bool IsCharging { get; set; }
        public double BatteryPercentage { get; set; }

        public TabletStatus()
        {
            TimeAdded = DateTime.UtcNow;
        }

        public TabletStatus(Controllers.ReturnObjects.GenericHitchBot context)
            : this()
        {
            TimeTaken = context.DateTime;
            HitchBotId = context.HitchBotId;
        }
    }
}
