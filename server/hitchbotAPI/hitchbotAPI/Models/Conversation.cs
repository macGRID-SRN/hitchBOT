using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Conversation
    {
        public int ID { get; set; }
        public Location StartedLocation { get; set; }
        public DateTime StartedTime { get; set; }
        [JsonIgnore]
        public List<ListenEvent> ListenEvents { get; set; }
        [JsonIgnore]
        public List<SpeechEvent> SpeechEvents { get; set; }
        public bool Complete { get; set; }

        public Conversation()
        {
            this.Complete = false;
        }
    }
}