using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        public string NearestCity { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonIgnore]
        public double? Altitude { get; set; }
        [JsonIgnore]
        public float? Accuracy { get; set; }
        public float? Velocity { get; set; }

        public DateTime TakenTime { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}