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
        public int ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public float Accuracy { get; set; }
        public float Velocity { get; set; }
        public DateTime TakenTime { get; set; }
    }
}