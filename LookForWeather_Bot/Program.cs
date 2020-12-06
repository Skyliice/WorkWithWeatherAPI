using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using WeatherConnect;

namespace LookForWeather_Bot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("1421567335:AAHUyMhHm4Kl6rcA65IO7Ldw0r4aWXCNGzM") { Timeout = TimeSpan.FromSeconds(10) };

            var me = botClient.GetMeAsync().Result;

            

            Console.WriteLine($"bot name: {me.FirstName}");
            
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            var s = botClient.MessageOffset;
            Console.ReadKey();
        }
       static int i;
        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e?.Message?.Text;
            i++;
            Console.WriteLine($"Message {i}: {message}");
            if (message == null)
                return;
            string[] words=message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            words[0]=words[0].ToLower();
            try
            {
                switch(words[0])
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Привет! Меня зовут WeatherNow и как ты уже понял я являюсь ботом. Моей основной (и единственной) задачей является отслеживание погоды в любой части света. Для начала работы со мной напиши команду: Погода <Название Города>");
                        break;
                    case "погода":
                        var CountryName = words[1];
                        var ApiConn = new WeatherConn();
                        ApiConn.GetWeather(CountryName).Wait();
                        await botClient.SendPhotoAsync(chatId: e.Message.Chat.Id, photo: "https://openweathermap.org/img/wn/" + WeatherInfo.CurWeather.WeatherNews[0].WeatherIcon + "@4x.png", caption: $"Выбранный город: {WeatherInfo.CurWeather.CityName}\nТекущая погода в цельсиях: {WeatherInfo.CurWeather.Main.CityTemperature}" +
                          $"\nПо ощущениям: {WeatherInfo.CurWeather.Main.TemperatureFeelslike}\nПогода: {WeatherInfo.CurWeather.WeatherNews[0].MainWeather}" +
                          $"\nДавление: {WeatherInfo.CurWeather.Main.Pressure}\nВлажность: {WeatherInfo.CurWeather.Main.Humidity}%\nСкорость ветра: {WeatherInfo.CurWeather.CurrentWind.Speed} м/с");
                        break;
                    case "помощь":
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Для работы с ботом необходимо написать команду в таком виде:\n Погода <Название Города>");
                        break;
                    default:
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Ошибка при обработке сообщения. Загляни в <Помощь> и проверь правильно ли ты написал команду!");
                        break;
                }
            }
            catch(Exception r)
            {
                Console.WriteLine(r);
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "Ошибка при обработке сообщения. Загляни в <Помощь> и проверь правильно ли ты написал команду!");
            }
            Console.WriteLine($"New text message: {message} in chat {e.Message.Chat.Id}");
            
        }
    }
}
