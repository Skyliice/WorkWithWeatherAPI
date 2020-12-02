﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithWeatherAPI
{
    public class Temperature
    {
        [JsonProperty(PropertyName="temp")]
        public double Temp { get; set; }
        [JsonProperty(PropertyName ="feels_like")]
        public double Feels_like { get; set; }
    }

    public class WeatherNow
    {
        [JsonProperty(PropertyName = "main")]
        public string Main;
        [JsonProperty(PropertyName = "description")]
        public string Description;
    }

    public class WeatherResponse
    {
        [JsonProperty(PropertyName ="main")]
        public Temperature Main;
        [JsonProperty(PropertyName = "name")]
        public string Name;
    }
}
