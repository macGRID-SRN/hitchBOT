using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Models
{
    public class Journey
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<HitchBot> HitchBots { get; set; }

        public virtual Location StartLocation { get; set; }
        public virtual Location EndLocation { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime TimeCreated { get; set; }

        public Journey()
        {
            this.TimeCreated = DateTime.UtcNow;
        }
    }
}
