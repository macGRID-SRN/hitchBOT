using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToTwitter;

namespace hitchbotAPI.Helpers
{
    public static class TwitterHelper
    {
        public static SingleUserAuthorizer GetAuthorization(int HitchBotID)
        {
            using (var db = new hitchbotAPI.Models.Database())
            {
                var TwitterAccount = db.TwitterAccounts.First(t => t.HitchBot.ID == HitchBotID);
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
    }
}