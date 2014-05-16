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
                var TwitterAccount = db.TwitterAccounts.Single(t => t.HitchBot.ID == HitchBotID);
                var Location = db.Locations.Single(l => l.ID == LocationID);

                var auth = new SingleUserAuthorizer
                {
                    CredentialStore = new SingleUserInMemoryCredentialStore
                    {
                        ConsumerKey = TwitterAccount.consumerKey,
                        ConsumerSecret = TwitterAccount.consumerSecret,
                        AccessToken = TwitterAccount.accessToken,
                        AccessTokenSecret = TwitterAccount.accessTokenSecret
                    }
                };

                try
                {
                    var twitterContext = new TwitterContext(auth);
                    var response = await twitterContext.TweetAsync(TweetText, (decimal)Location.Latitude, (decimal)Location.Longitude, true);

                }
                catch (TwitterQueryException e)
                {
                    return string.Format("Consumer: {0} Secret: {1} API: {2} Secret: {3} Dump: {4}", auth.CredentialStore.ConsumerKey, auth.CredentialStore.ConsumerSecret, auth.CredentialStore.OAuthToken, auth.CredentialStore.OAuthTokenSecret, e.ToString());
                }
            }
            return "Tweet sent successfully.";
        }
    }
}