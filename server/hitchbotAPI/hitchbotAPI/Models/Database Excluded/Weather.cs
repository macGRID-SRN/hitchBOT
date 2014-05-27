using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Models.Database_Excluded
{
    public class Weather
    {
        public List<WeatherDescription> WeatherDescriptions = new List<WeatherDescription>();
        public string temperature;
        public string humidity;
        public string pressure;
        public string sunset;
        public string sunrise;
        public string windSpeed;
        public string windSpeedGust;
        public string windDirection;

        public Weather(dynamic json)
        {
            dynamic sys = json["sys"];
            dynamic weather = json["weather"];
            dynamic main = json["main"];
            dynamic wind = json["wind"];

            this.sunrise = sys["sunrise"].ToString();
            this.sunset = sys["sunset"].ToString();

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
            this.humidity = main["humidity"].ToString();
            this.pressure = main["pressure"].ToString();
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