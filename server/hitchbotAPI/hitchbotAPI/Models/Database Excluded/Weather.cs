using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Collections;

namespace hitchbotAPI.Models.Database_Excluded
{
    //Under no circumstance should a user use the characters 8b8
    public class Weather
    {
        public List<WeatherDescription> WeatherDescriptions = new List<WeatherDescription>();
        public string temperature;
        public decimal temperatureActual;
        public string humidity;
        public string pressure;
        public string sunset;
        public string sunrise;
        public string windSpeed;
        public string windSpeedGust;
        public string windDirection;
        public string CityName;
        public const string timeZoneUrl = "https://maps.googleapis.com/maps/api/timezone/json?";

        private const decimal KELVIN = 273.15M;

        public Weather(dynamic json, string cityName = null)
        {
            dynamic sys = json["sys"];
            dynamic weather = json["weather"];
            dynamic main = json["main"];
            dynamic wind = json["wind"];

            decimal lat = decimal.Parse(json["coord"]["lat"].ToString());
            decimal lon = decimal.Parse(json["coord"]["lon"].ToString());

            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["location"] = String.Join(",", lat.ToString(), lon.ToString());
            queryString["timestamp"] = sys["sunrise"].ToString();
            queryString["sensor"] = "true";

            string jsonString = Helpers.WebHelper.GetRequest(timeZoneUrl + queryString.ToString());
            dynamic timeOffsetJson = Helpers.WebHelper.GetJSON(jsonString);
            dynamic totalTimeOffset = timeOffsetJson["dstOffset"] + timeOffsetJson["rawOffset"];

            if (String.IsNullOrEmpty(cityName))
            {
                this.CityName = json["name"].ToString();
            }
            else
                this.CityName = cityName;

            this.sunrise = ((string)Helpers.TimeHelper.GetHourFromUnixTime((sys["sunrise"] + totalTimeOffset))).Replace(":","8b8");
            this.sunset = ((string)Helpers.TimeHelper.GetHourFromUnixTime((sys["sunset"] + totalTimeOffset))).Replace(":", "8b8");

            foreach (dynamic weatherItem in weather)
            {
                this.WeatherDescriptions.Add(new WeatherDescription(weatherItem));
            }

            this.windSpeed = wind["speed"].ToString();
            this.windDirection = wind["deg"].ToString();
            try
            {
                this.windSpeedGust = wind["gust"].ToString();
            }
            catch (KeyNotFoundException e)
            {
                this.windSpeedGust = null;
            }

            this.temperature = main["temp"].ToString();
            this.temperatureActual = decimal.Parse(this.temperature) - KELVIN;
            this.humidity = main["humidity"].ToString();


            this.pressure = main["pressure"].ToString();
        }

        public string ToString(string CityName = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("It is ");
            sb.Append(this.temperatureActual + "°C ");
            sb.Append("in ");

            if (String.IsNullOrEmpty(CityName))
                sb.Append(this.CityName);
            else
                sb.Append(CityName);

            sb.Append(". ");

            return sb.ToString();
        }

        public IEnumerable<string> GetIterator()
        {
            yield return string.Join("|", new string[] { this.CityName, this.temperatureActual.ToString(), this.humidity });
            yield return string.Join("|", new string[] { this.pressure, this.sunset, this.sunrise });
            yield return string.Join("|", new string[] { this.windSpeed, this.windDirection, this.temperature });
            if (this.WeatherDescriptions.Count > 0)
            {
                yield return string.Join("|", new string[] { this.WeatherDescriptions[0].main, this.WeatherDescriptions[0].description });
            }
            else
                yield return " | ";
        }

        public class WeatherDescription
        {
            public string id;
            public string main;
            public string description;

            public WeatherDescription(dynamic json)
            {
                this.id = json["id"].ToString();
                this.main = json["main"].ToString();
                this.description = json["description"].ToString();
            }
        }
    }
}