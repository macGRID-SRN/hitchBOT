using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Models;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming
namespace hitchbot_secure_api.Helpers
{
    public static class WeatherHelper
    {

        public class OpenWeatherApi
        {

            private const string AppId = "f4710ad75291edad6d38785b4cdcb315"; //The APPID
            private owData _owCurrentWeather; //Holds the deserialized data

            //From: http://www.codeproject.com/Tips/397574/Use-Csharp-to-get-JSON-Data-from-the-Web-and-Map-i


            public VariableValuePair GetCityNamePair()
            {
                return new VariableValuePair
                {
                    key = "current_city_name",
                    value = GetCityName()
                };
            }

            public VariableValuePair GetTempFPair()
            {
                return new VariableValuePair
                {
                    key = "weather_temperatureC",
                    value = Convert.ToInt32(GetTempInC() * 1.8 + 32).ToString()
                };
            }

            public VariableValuePair GetTempTextFPair()
            {
                return new VariableValuePair
                {
                    key = "weather_ctext",
                    value = Helpers.HumanFriendlyInteger.IntegerToWritten(Convert.ToInt32(GetTempInC() * 1.8 + 32))
                };
            }

            public VariableValuePair GetTempCPair()
            {
                return new VariableValuePair
                {
                    key = "weather_temperatureC",
                    value = Convert.ToInt32(GetTempInC()).ToString()
                };
            }

            public VariableValuePair GetTempTextCPair()
            {
                return new VariableValuePair
                {
                    key = "weather_ctext",
                    value = Helpers.HumanFriendlyInteger.IntegerToWritten(Convert.ToInt32(GetTempInC()))
                };
            }

            public VariableValuePair GetWeatherStatusPair()
            {
                return new VariableValuePair
                {
                    key = "weather_status",
                    value = GetWeatherCondition()
                };
            }

            //Load the data from a JSON request
            public void LoadWeatherData(double lat, double lon)
            {
                try
                {
                    string url = "http://api.openweathermap.org/data/2.5/weather?APPID=" + AppId + "&lat=" + lat + "&lon=" + lon;
                    _owCurrentWeather = WebApiHelper._download_serialized_json_data<owData>(url);
                }
                catch (Exception er)
                {
                    Console.WriteLine("Error loading current weather for lat {0}, lon {1}. Message: {2}", lat, lon, er.Message);
                }

            }

            public string GetWeatherCondition()
            {
                var weatherConds = "";

                try
                {
                    //Not sure why a list is used by the OpenWeather site, but it is probably down to the ability for multiple conditions to exist at once
                    weatherConds = _owCurrentWeather.weather[0].main;
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return weatherConds;
            }
            //Get the current weather text in a CSV string
            public string GetWeatherCondsAsCsv()
            {
                var weatherConds = "";

                try
                {
                    //Not sure why a list is used by the OpenWeather site, but it is probably down to the ability for multiple conditions to exist at once
                    weatherConds = _owCurrentWeather.weather.Aggregate(weatherConds, (current, wListItem) => current + (wListItem.main + ","));

                    weatherConds = weatherConds.Substring(0, weatherConds.Length - 1); //Shed the last comma
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return weatherConds; //Send back the data
            }

            //Get the integer values as a CSV string
            public string GetWeatherIdsAsCsv()
            {
                var weatherIDs = "";


                try
                {
                    //Not sure why a list is used by the OpenWeather site, but it is probably down to the ability for multiple conditions to exist at once
                    weatherIDs = _owCurrentWeather.weather.Aggregate(weatherIDs, (current, wListItem) => current + (wListItem.id.ToString() + ","));

                    weatherIDs = weatherIDs.Substring(0, weatherIDs.Length - 1); //Shed the last comma
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return weatherIDs; //Send back the data
            }

            public double GetTempInK()
            {
                var temp = 0d;
                try
                {
                    temp = _owCurrentWeather.main.temp;
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return temp;
            }

            public double GetTempInC()
            {
                var temp = 0d;
                try
                {
                    temp = _owCurrentWeather.main.temp - 273;
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return temp;
            }

            public double GetHumidity()
            {
                var humidity = 0d;
                try
                {
                    humidity = _owCurrentWeather.main.humidity;
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return humidity;
            }

            public string GetCityName()
            {
                var name = "";
                try
                {
                    name = _owCurrentWeather.name.ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception er)
                {
                    Console.WriteLine("ERROR: {0}", er.Message);
                }

                return name;
            }

        }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double humidity { get; set; }
            public double pressure { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public double deg { get; set; }
        }

        public class Rain
        {
            public int __invalid_name__3h { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class owData
        {
            public Coord coord { get; set; }
            public Sys sys { get; set; }
            public List<Weather> weather { get; set; }
            public Main main { get; set; }
            public Wind wind { get; set; }
            public Rain rain { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
    }
}
