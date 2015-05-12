using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace hitchbotAPI.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        public string NearestCity { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [JsonIgnore]
        public double? Altitude { get; set; }

        [JsonIgnore]
        public float? Accuracy { get; set; }

        public float? Velocity { get; set; }
        public virtual hitchBOT HitchBOT { get; set; }
        public DateTime TakenTime { get; set; }
        public DateTime TimeAdded { get; set; }
    }

    //used http://json2csharp.com/ to get this. haha feels like such a dirty cheat.
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpotApiCall
    {
        public class Feed
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string status { get; set; }
            public int usage { get; set; }
            public int daysRange { get; set; }
            public bool detailedMessageShown { get; set; }
        }

        public class Message
        {
            public int id { get; set; }
            public string messengerId { get; set; }
            public string messengerName { get; set; }
            public int unixTime { get; set; }
            public string messageType { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string modelId { get; set; }
            public string showCustomMsg { get; set; }
            public string dateTime { get; set; }
            public string batteryState { get; set; }
            public int hidden { get; set; }
            public string messageContent { get; set; }
        }

        public class FeedMessageResponse
        {
            public int count { get; set; }
            public Feed feed { get; set; }
            public int totalCount { get; set; }
            public int activityCount { get; set; }
            public ICollection<Message> messages { get; set; }
        }
    }
}