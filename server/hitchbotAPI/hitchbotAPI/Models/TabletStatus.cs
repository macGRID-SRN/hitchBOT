using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class TabletStatus
    {
        public int ID { get; set; }
        public virtual hitchBOT HitchBOT { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime TimeAdded { get; set; }
        public double BatteryTemp { get; set; }
        public double BatteryVoltage { get; set; }
        public bool IsCharging { get; set; }
        public double BatteryPercentage { get; set; }

    }
}