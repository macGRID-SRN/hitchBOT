using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Diagnostics;

namespace hitchbotAPI.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class TwitterApiController : ApiController
    {
        [HttpPost]
        public async Task<string> PostTweetWithLocation(int HitchBotID, int LocationID, string TweetText)
        {
            int TweetID = await Helpers.TwitterHelper.PostTweetWithLocation(HitchBotID, LocationID, TweetText);

            if (TweetID != 0) { return "Tweet sent successfully. ID: " + TweetID; }
            return "Something went wrong!";
        }
    }
}