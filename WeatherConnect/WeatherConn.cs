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
            WebRequest request = WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?q={CountryName}&units=metric&APPID={AppID}");
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();
            using (var s = response.GetResponseStream())
            {
                using (var reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            WeatherInfo.CurWeather = JsonConvert.DeserializeObject<WeatherResponse>(answer);            
            response.Close();
        }
    }
}
