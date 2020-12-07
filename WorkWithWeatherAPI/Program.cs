using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherConnect;
using System.Configuration;

namespace WorkWithWeatherAPI
{
    class Program
    {
        static string CountryName;
        static void Main(string[] args)
        {
            var WeatherCon = new WeatherConn();
            Console.Write("Введите название города: ");
            CountryName = Console.ReadLine();
            try
            {
                WeatherCon.GetWeather(CountryName, ConfigurationSettings.AppSettings["appId"]).Wait();
                Console.WriteLine("Успешно");
                Console.WriteLine($"Город: {WeatherInfo.CurWeather.CityName}\nТемпература:{WeatherInfo.CurWeather.Main.CityTemperature}");
            }
            catch
            {
                Console.Write("Город не найден.");
            }
            Console.ReadKey();
        }

        
        
    }
}
