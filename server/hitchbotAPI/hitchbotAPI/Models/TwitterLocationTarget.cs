using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Models
{
    public class TwitterLocationTarget
    {
        public int ID { get; set; }
        public Location TargetLocation { get; set; }
        public hitchBOT HitchBot { get; set; }
        public string TweetText { get; set; }
        public double RadiusKM { get; set; }
        public TwitterStatus Status { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}