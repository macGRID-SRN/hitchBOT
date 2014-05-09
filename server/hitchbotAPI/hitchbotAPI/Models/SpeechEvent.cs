using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Models
{
    public class SpeechEvent
    {
        public int ID { get; set; }
        public string SpeechSaid { get; set; }
        public DateTime OccuredTime { get; set; }
    }
}