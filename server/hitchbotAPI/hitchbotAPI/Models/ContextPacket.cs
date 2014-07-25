using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class ContextPacket
    {
        public List<KeyValuePair> data;

        public ContextPacket(List<KeyValuePair> myValue)
        {
            this.data = myValue;
        }
    }

    public class KeyValuePair
    {
        public string key { get; set; }
        public string value { get; set; }

        public KeyValuePair(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}