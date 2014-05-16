using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class TwitterAccount
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string UserID { get; set; }
        [JsonIgnore]
        public string consumerKey { get; set; }
        [JsonIgnore]
        public string consumerSecret { get; set; }
        [JsonIgnore]
        public string accessToken { get; set; }
        [JsonIgnore]
        public string accessTokenSecret { get; set; }
        [JsonIgnore]
        public hitchBOT HitchBot { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}