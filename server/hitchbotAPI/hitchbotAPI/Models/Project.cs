using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Project
    {
        public int ID { get; set; }
        [JsonIgnore]
        public virtual List<hitchBOT> hitchBOTs { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public Location StartLocation { get; set; }
        public DateTime? EndTime { get; set; }
        public Location EndLocation { get; set; }
        public string Description { get; set; }
    }
}