using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class SpeechEvent
    {
        public int ID { get; set; }
        public string SpeechSaid { get; set; }
        public DateTime OccuredTime { get; set; }
    }
}