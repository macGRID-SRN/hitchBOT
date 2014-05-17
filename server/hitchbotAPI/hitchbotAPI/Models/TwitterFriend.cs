﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace hitchbotAPI.Models
{
    public class TwitterFriend
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string ScreenName { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime? TimeFollowed { get; set; }

    }
}