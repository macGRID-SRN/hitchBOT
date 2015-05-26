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
        public int JourneyId { get; set; }
        [Required]
        public virtual Journey Journey { get; set; }
    }
}
