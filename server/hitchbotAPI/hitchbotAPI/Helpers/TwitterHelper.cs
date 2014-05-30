using System;
using hitchbotAPI;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToTwitter;
using System.Diagnostics;
using System.Threading.Tasks;

namespace hitchbotAPI.Helpers
{
    public static class TwitterHelper
    {
        public static async Task<int> PostTweetWithLocationAndWeather(int HitchBotID, int LocationID)
        {
            string URL;
            using (var db = new Models.Database())
            {
                var location = db.Locations.First(l => l.ID == LocationID);
                URL = "http://api.openweathermap.org/data/2.5/weather?lat=" + location.Latitude + "&lon=" + location.Longitude;
                dynamic weather = Helpers.WebHelper.GetJSON(Helpers.WebHelper.GetRequest(URL));
                Models.Database_Excluded.Weather WeatherEvent = new Models.Database_Excluded.Weather(weather, location.NearestCity);

                return await PostTweetWithLocation(HitchBotID, LocationID, CleverScriptHelper.GetWeatherTweet(WeatherEvent, 1));
            }
        }

        public static async Task<int> PostTweetWithLocation(int HitchBotID, int LocationID, string TweetText)
        {
            using (var db = new Models.Database())
            {
                var Location = db.Locations.First(l => l.ID == LocationID);

                try
                {
                    string UserID;
                    var twitterContext = GetContext(HitchBotID, out UserID);
                    Status response = await twitterContext.TweetAsync(TweetText, (decimal)Location.Latitude, (decimal)Location.Longitude, true);
                    if (string.IsNullOrEmpty(Location.NearestCity))
                        Location.NearestCity = response.Place.FullName;
                    db.SaveChanges();
                    return AddTweetToDatabase(UserID, response);
                }
                //catch (TwitterQueryException e)
                //{
                //    return e.ToString();
                //}
                //catch (InvalidOperationException e)
                //{
                //    return e.ToString();
                //}
                //catch (System.Data.SqlClient.SqlException e)
                //{
                //    return e.ToString();
                //}
                //catch (System.NotSupportedException e)
                //{
                //    return e.ToString();
                //}
                catch (Exception e)
                {
                }
            }
            return 0;
        }

        public static int AddTweetToDatabase(string UserID, Status newStatus)
        {
            using (var db = new Models.Database())
            {
                var TwitterStatus = new Models.TwitterStatus()
                {
                    TwitterAccount = db.TwitterAccounts.First(ta => ta.UserID == UserID),
                    TweetID = newStatus.StatusID.ToString(),
                    Text = newStatus.Text,
                    TimeTweeted = newStatus.CreatedAt,
                    TimeAdded = DateTime.UtcNow
                };

                db.TwitterStatuses.Add(TwitterStatus);
                db.SaveChanges();
                return TwitterStatus.ID;
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