using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Configuration;
using LinqToTwitter;
using System.Threading.Tasks;

namespace hitchbotAPI.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class TwitterApiController : ApiController
    {
        [HttpPost]
        public bool PostTweetWithLocation(int HitchBotID, int LocationID, string TweetText)
        {
            using (var db = new Models.Database())
            {
                var TwitterAccount = db.TwitterAccounts.Single(t => t.HitchBot.ID == HitchBotID);
                var Location = db.Locations.Single(l => l.ID == LocationID);

                var auth = new SingleUserAuthorizer
                {
                    CredentialStore = new SingleUserInMemoryCredentialStore
                    {
                        ConsumerKey = TwitterAccount.consumerKey,
                        ConsumerSecret = TwitterAccount.consumerSecret,
                        AccessToken = ConfigurationManager.AppSettings["accessToken"],
                        AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
                    }
                };

                try
                {
                    var twitterContext = new TwitterContext(auth);
                    var response = twitterContext.TweetAsync(TweetText, (decimal)Location.Latitude, (decimal)Location.Longitude, true);
                }
                catch (TwitterQueryException e)
                {

                }
            }
            return true;
        }
    }
}