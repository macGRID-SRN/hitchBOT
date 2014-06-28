using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections.Specialized;


namespace hitchbotAPI.Helpers
{
    public static class CleverScriptHelper
    {
        public const string testURl = "http://testapi.cleverscript.com/csapi?";
        public const string weatherKey = "Weather";

        public static string GetWeatherTweet(Models.Database_Excluded.Weather Weather, int HitchBotID)
        {
            string output = string.Empty;
            using (var db = new Models.Database())
            {
                var CleverScriptForHitchBOT = db.CleverScriptAPIkeys.Where(cs => cs.HitchBOT.ID == HitchBotID);
                var CleverScriptKey = CleverScriptForHitchBOT.First(cs => cs.Description == weatherKey);
                string lastCS = string.Empty;
                foreach (string URLinput in Weather.GetIterator())
                {
                    NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                    queryString["key"] = CleverScriptKey.APIkey;
                    queryString["input"] = URLinput;
                    if (!String.IsNullOrEmpty(lastCS))
                        queryString["cs"] = lastCS;

                    dynamic json = WebHelper.GetJSON(WebHelper.GetRequest(testURl + queryString.ToString()));
                    lastCS = json["cs"].ToString();
                    output = json["output"];
                }
            }
            return HttpUtility.HtmlDecode(output).Replace("8b8", ":");
        }

        public static string GetNewFollowTweet(int HitchBotID)
        {
            return String.Empty;
        }
    }
}