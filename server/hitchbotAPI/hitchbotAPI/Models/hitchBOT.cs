using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace hitchbotAPI.Models
{
    public class hitchBOT
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<SpeechEvent> SpeechEvents { get; set; }
        public List<Location> Locations { get; set; }
    }
}