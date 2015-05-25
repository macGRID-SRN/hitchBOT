using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class ContextPacket
    {
        public List<KeyValuePair> data;
    }

    public class KeyValuePair
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
