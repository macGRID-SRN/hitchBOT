using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Helpers
{
    public static class WebApiHelper
    {
        public static T GetObjectFromWeb<T>(string url) where T : new()
        {
            return _download_serialized_json_data<T>(url);
        }

        public static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                string jsonData = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    jsonData = w.DownloadString(url);
                }
                catch (Exception)
                {
                    // ignored
                }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            }
        }
    }
}
