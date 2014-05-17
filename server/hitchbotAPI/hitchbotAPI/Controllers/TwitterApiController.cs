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
            using (var db = new Models.Database())
            {
                var Location = db.Locations.First(l => l.ID == LocationID);

                try
                {
                    string UserID;
                    var twitterContext = Helpers.TwitterHelper.GetContext(HitchBotID, out UserID);
                    Status response = await twitterContext.TweetAsync(TweetText, (decimal)Location.Latitude, (decimal)Location.Longitude, true);

                    Helpers.TwitterHelper.AddTweetToDatabase(UserID, response);
                }
                catch (TwitterQueryException e)
                {
                    return e.ToString();
                }
                catch (InvalidOperationException e)
                {
                    return e.ToString();
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    return e.ToString();
                }
                catch (System.NotSupportedException e)
                {
                    return e.ToString();
                }
            }
            return "Tweet sent successfully.";
        }
    }
}