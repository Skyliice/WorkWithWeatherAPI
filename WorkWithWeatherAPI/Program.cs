using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithWeatherAPI
{
    class Program
    {
        //ID Санкт-петербурга: 498817
        //ID Москвы: 524901
        static string CountryID;
        static void Main(string[] args)
        {
            Console.Write("Введите ID города: ");
            CountryID = Console.ReadLine();
            try
            {
                ConnectAsync().Wait();
                Console.WriteLine("Успешно");
            }
            catch
            {
                Console.Write("Город не найден.");
            }
            Console.ReadKey();
        }

        const string AppID = "83bcb7916396f617361adf11fc02cd33";
        public static async Task ConnectAsync()
        {
           
            string answer;
            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?id=" + CountryID + "&units=metric&APPID=" + AppID);
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            WeatherResponse wrresponse = JsonConvert.DeserializeObject<WeatherResponse>(answer);
            Console.WriteLine("Текущая погода в городе "+wrresponse.name +": "+wrresponse.main.temp +" градусов по цельсию.\nПо ощущениям: "+wrresponse.main.feels_like+" градусов.");
            response.Close();
        }
    }
}
