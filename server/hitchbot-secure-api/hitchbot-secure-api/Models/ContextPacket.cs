using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Models
{
    public class ContextPacket
    {
        public int Id { get; set; }

        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public virtual ICollection<VariableValuePair> Variables { get; set; }

        public DateTime TimeCreated { get; set; }

        public ContextPacket()
        {
            TimeCreated = DateTime.UtcNow;
        }
    }

    public class VariableValuePair
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string key { get; set; }
        public string value { get; set; }

        [JsonIgnore]
        public int ContextPacketId { get; set; }
        [JsonIgnore]
        public ContextPacket ContextPacket { get; set; }
    }
}
