using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class ListenEvent
    {
        public int ID { get; set; }
        public string SpeechHeard { get; set; }
        public DateTime HeardTime { get; set; }
    }
}