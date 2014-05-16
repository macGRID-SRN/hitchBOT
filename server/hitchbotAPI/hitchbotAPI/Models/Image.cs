using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string url { get; set; }
        public virtual Location location { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}