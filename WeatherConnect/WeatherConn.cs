using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConnect
{
    public class WeatherConn
    {
        private const string AppID = "83bcb7916396f617361adf11fc02cd33";
        public async Task GetWeather(string CountryName)
        {
            string answer;
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + CountryName + "&units=metric&APPID=" + AppID);
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            WeatherInfo.CurWeather = JsonConvert.DeserializeObject<WeatherResponse>(answer);
            //Console.WriteLine("Текущая погода в городе " + CurWeather.CityName + ": " + CurWeather.MainTemperature.CityTemperature + " градусов по цельсию.\nПо ощущениям: " + CurWeather.MainTemperature.TemperatureFeelslike + " градусов.");
            
            response.Close();
        }
    }
}
