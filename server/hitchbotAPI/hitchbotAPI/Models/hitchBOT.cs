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
        public virtual List<Location> Locations { get; set; }
        [JsonIgnore]
        public virtual List<Conversation> Conversations { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}