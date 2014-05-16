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

namespace hitchbotAPI.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class TwitterApiController : ApiController
    {
        [HttpPost]
        public async Task<string> PostTweetWithLocation(int HitchBotID, int LocationID, string TweetText)
        {
            using (var db = new Models.Database())
            {
                var Location = db.Locations.Single(l => l.ID == LocationID);

                try
                {
                    var twitterContext = Helpers.TwitterHelper.GetContext(HitchBotID);
                    var response = await twitterContext.TweetAsync(TweetText, (decimal)Location.Latitude, (decimal)Location.Longitude, true);
                    return "Tweet from: " + response.Place.Name;
                }
                catch (TwitterQueryException e)
                {
                    return e.ToString();
                }
            }
            return "Tweet sent successfully.";
        }
    }
}