using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToTwitter;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace hitchbotAPI.Models
{
    public class TwitterStatus
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string TweetID { get; set; }
        public TwitterAccount TwitterAccount { get; set; }
        [MaxLength(140)]
        public string Text { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime TimeTweeted { get; set; }

        //public TwitterStatus(string UserID, Status myStatus)
        //{
        //    //it is written in the twitter documentation that it should be stored as a string due to 53 bit limitations of some languages (eg javascript and serialization JSON)
        //    this.TweetID = myStatus.StatusID.ToString();
        //    using (var db = new Database())
        //    {
        //        this.TwitterAccount = db.TwitterAccounts.First(ta => ta.UserID == UserID);
        //    }
        //    this.Text = myStatus.Text;
        //    this.TimeAdded = DateTime.UtcNow;
        //    this.TimeTweeted = myStatus.CreatedAt;
        //}
    }
}