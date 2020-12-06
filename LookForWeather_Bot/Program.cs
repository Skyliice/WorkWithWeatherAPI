using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using WeatherConnect;

namespace LookForWeather_Bot
{
    class Program
    {
        private static ITelegramBotClient botClient;
        static async Task Main(string[] args)
        {
            botClient = new TelegramBotClient("1421567335:AAHUyMhHm4Kl6rcA65IO7Ldw0r4aWXCNGzM") { Timeout = TimeSpan.FromSeconds(10) };
            await botClient.GetMeAsync();
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            var s = botClient.MessageOffset;
            Console.ReadKey();
        }

        async static void ShowWeather(long ChatId,string CountryName)
        {
            var ApiConn = new WeatherConn();
            ApiConn.GetWeather(CountryName).Wait();
            var Imgurl = $"https://openweathermap.org/img/wn/{WeatherInfo.CurWeather.WeatherNews[0].WeatherIcon}@4x.png";
            var Citystr = $"Выбранный город: {WeatherInfo.CurWeather.CityName}";
            var CurTemp = $"\nТекущая температура в цельсиях: {WeatherInfo.CurWeather.Main.CityTemperature}";
            var CurFeelsTemp= $"\nПо ощущениям: {WeatherInfo.CurWeather.Main.TemperatureFeelslike}";
            var CurWeatherr = $"\nПогода: {WeatherInfo.CurWeather.WeatherNews[0].MainWeather}";
            var CurPressure = $"\nДавление: {WeatherInfo.CurWeather.Main.Pressure}";
            var CurHumidity = $"\nВлажность: {WeatherInfo.CurWeather.Main.Humidity}%";
            var CurWindSpeed = $"\nСкорость ветра: {WeatherInfo.CurWeather.CurrentWind.Speed} м/с";
            var FinalString = String.Concat(Citystr, CurTemp, CurFeelsTemp, CurWeatherr, CurPressure, CurHumidity, CurWindSpeed);
            await botClient.SendPhotoAsync(chatId: ChatId, photo: Imgurl, caption: FinalString);
        }

        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e?.Message?.Text;
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
                        ShowWeather(e.Message.Chat.Id,words[1]);
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
