using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherConnect;

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
                WeatherCon.GetWeather(CountryName).Wait();
                Console.WriteLine("Успешно");
            }
            catch
            {
                Console.Write("Город не найден.");
            }
            Console.ReadKey();
        }

        
        
    }
}
