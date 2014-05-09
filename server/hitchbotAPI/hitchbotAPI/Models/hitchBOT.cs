using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class hitchBOT
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        [JsonIgnore]
        public List<SpeechEvent> SpeechEvents { get; set; }
        [JsonIgnore]
        public List<Location> Locations { get; set; }
    }
}