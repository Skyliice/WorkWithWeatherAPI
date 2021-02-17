using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConnect
{
    public class Temperature
    {
        [JsonProperty(PropertyName="temp")]
        public double CityTemperature { get; set; }
        [JsonProperty(PropertyName ="feels_like")]
        public double TemperatureFeelslike { get; set; }
        [JsonProperty(PropertyName ="pressure")]
        public double Pressure { get; set; }
        [JsonProperty(PropertyName ="humidity")]
        public double Humidity { get; set; }
    }

    public class WeatherNow
    {
        [JsonProperty(PropertyName = "main")]
        public string MainWeather { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string WeatherDescription { get; set; }
        [JsonProperty(PropertyName ="icon")]
        public string WeatherIcon { get; set; }
    }

    public class Wind
    {
        [JsonProperty(PropertyName ="speed")]
        public double Speed { get; set; }
    }
    public class WeatherResponse
    {
        [JsonProperty(PropertyName ="main")]
        public Temperature Main { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string CityName { get; set; }
        
        [JsonProperty(PropertyName ="wind")]
        public Wind CurrentWind { get; set; }

        [JsonProperty(PropertyName ="weather")]
        public WeatherNow[] WeatherNews { get; set; }
    }
}
