using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Models
{
    public class HitchBot
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<SpeechLogEvent> SpeechLogEvents { get; set; }
        public virtual ICollection<CleverscriptContent> CleverscriptContents { get; set; }
        public virtual ICollection<CleverscriptContext> CleverscriptContexts { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        //public virtual ICollection<TabletSerial> TabletSerials { get; set; }
        public int JourneyId { get; set; }
        [Required]
        public virtual Journey Journey { get; set; }
    }
}
