using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Models
{
    public class CleverScriptAPIkey
    {
        public int ID { get; set; }
        public string APIkey { get; set; }
        public string Description { get; set; }
        public hitchBOT HitchBOT { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}