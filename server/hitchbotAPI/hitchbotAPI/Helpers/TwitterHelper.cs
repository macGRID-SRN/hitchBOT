using System;
using hitchbotAPI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToTwitter;
using System.Diagnostics;

namespace hitchbotAPI.Helpers
{
    public static class TwitterHelper
    {
        public static void AddTweetToDatabase(string UserID, Status newStatus)
        {
            using (var db = new Models.Database())
            {
                try
                {
                Debug.WriteLine("Adding Tweet..");
                var TwitterStatus = new Models.TwitterStatus()
                {
                    TwitterAccount = db.TwitterAccounts.First(ta => ta.UserID == UserID),
                    TweetID = newStatus.StatusID.ToString(),
                    Text = newStatus.Text,
                    TimeTweeted = newStatus.CreatedAt,
                    TimeAdded = DateTime.UtcNow
                };
                Debug.WriteLine("Tweet built.");
                
                    db.TwitterStatuses.Add(TwitterStatus);
                    db.SaveChanges();
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
        }

        public static SingleUserAuthorizer GetAuthorization(Models.TwitterAccount TwitterAccount, out string UserID)
        {
            UserID = TwitterAccount.UserID;
            using (var db = new Models.Database())
            {
                return new SingleUserAuthorizer()
                {
                    CredentialStore = new SingleUserInMemoryCredentialStore
                    {
                        ConsumerKey = TwitterAccount.consumerKey,
                        ConsumerSecret = TwitterAccount.consumerSecret,
                        AccessToken = TwitterAccount.accessToken,
                        AccessTokenSecret = TwitterAccount.accessTokenSecret
                    }
                };
            }
        }

        public static TwitterContext GetContext(int HitchBotID, out string UserID)
        {
            using (var db = new Models.Database())
            {
                return new TwitterContext(GetAuthorization(db.TwitterAccounts.First(ta => ta.HitchBot.ID == HitchBotID), out UserID));
            }
        }
    }
}