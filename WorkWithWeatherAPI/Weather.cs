using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithWeatherAPI
{
    public class Temperature
    {
        public double temp;
        public double feels_like;
    }

    public class WeatherNow
    {
        public string main;
        public string description;
    }

    public class WeatherResponse
    {
        public Temperature main;
        public string name;
        public WeatherNow[] weather;
    }
}
