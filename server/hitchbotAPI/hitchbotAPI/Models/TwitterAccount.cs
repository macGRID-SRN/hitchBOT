using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class TwitterAccount
    {
        public int ID { get; set; }
        public string consumerKey { get; set; }
        [JsonIgnore]
        public string consumerSecret { get; set; }
        [JsonIgnore]
        public hitchBOT HitchBot { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}