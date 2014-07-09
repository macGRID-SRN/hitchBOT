using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class GoogleMapsStatic
    {
        public int ID { get; set; }
        public hitchBOT HitchBot { get; set; }
        public string URL { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeAdded { get; set; }
        public int ViewCount { get; set; }
        public string NearestCity { get; set; }

        public GoogleMapsStatic()
        {
            this.TimeAdded = DateTime.UtcNow;
        }
    }
}