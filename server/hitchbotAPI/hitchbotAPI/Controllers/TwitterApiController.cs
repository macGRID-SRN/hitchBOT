using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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

        [HttpPost]
        public async Task<string> PostTweetWithLocation(int HitchBotID, int LocationID)
        {
            int TweetID = await Helpers.TwitterHelper.PostTweetWithLocationAndWeather(HitchBotID, LocationID);

            if (TweetID != 0) { return "Tweet sent successfully. ID: " + TweetID; }
            return "Something went wrong!";
        }

        [HttpGet]
        public async Task<string> checkFollowers(int HitchBotID)
        {
            var twitterCtx = Helpers.TwitterHelper.GetContext(HitchBotID);

            using (var db = new Models.Database())
            {
                var TwitterAccount = db.TwitterAccounts.First(ta => ta.HitchBot.ID == HitchBotID);
                string UserID = TwitterAccount.UserID;
                var friendship = await
                (from friend in twitterCtx.Friendship
                 where friend.Type == FriendshipType.FollowersList && friend.UserID == UserID
                 select friend).SingleOrDefaultAsync();
                var twitterFriends = db.TwitterFriends.Where(tf => tf.TwitterAccount.ID == TwitterAccount.ID);
                foreach (LinqToTwitter.User myUser in friendship.Users)
                {
                    string tempUserID = myUser.UserIDResponse.ToString();
                    if (!db.TwitterFriends.Any(tu => tu.UserID == tempUserID))
                    {
                        User x = await twitterCtx.CreateFriendshipAsync(myUser.ScreenNameResponse, true);
                        db.TwitterFriends.Add(
                            new Models.TwitterFriend()
                            {
                                UserID = myUser.UserIDResponse.ToString(),
                                ScreenName = myUser.ScreenNameResponse,
                                TwitterAccount = TwitterAccount,
                                TimeAdded = DateTime.UtcNow,
                                TimeFollowed = DateTime.UtcNow
                            });

                    }


                }
                db.SaveChanges();
                return "Success";
            }

        }
    }
}