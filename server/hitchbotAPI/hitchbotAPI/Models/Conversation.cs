using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Conversation
    {
        public int ID { get; set; }
        public Location StartLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public virtual hitchBOT HitchBOT { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Complete
        {
            get
            {
                return EndTime != null;
            }
        }
        [JsonIgnore]
        public virtual List<ListenEvent> ListenEvents { get; set; }
        [JsonIgnore]
        public virtual List<SpeechEvent> SpeechEvents { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}