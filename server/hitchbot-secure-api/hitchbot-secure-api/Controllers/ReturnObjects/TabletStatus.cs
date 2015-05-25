using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Controllers.ReturnObjects;

namespace hitchbot_secure_api.Controllers
{
    public partial class TabletController
    {
        public class ReturnTabletStatus : GenericHitchBot
        {
            public double BatteryTemp { get; set; }
            public double BatteryVoltage { get; set; }
            public bool IsCharging { get; set; }
            public double BatteryPercentage { get; set; }
        }
    }
}
