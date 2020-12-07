using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WeatherConnect
{
    public class WeatherConn
    {
        public async Task GetWeather(string CountryName,string appId)
        {
            string answer;
            var url = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}", CountryName, appId);
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var response = await request.GetResponseAsync();
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
