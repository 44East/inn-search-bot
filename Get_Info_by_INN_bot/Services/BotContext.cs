using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Get_Info_by_INN_bot.Services
{
    internal class BotContext
    {
        private Dictionary<long, bool> _awaitingINN;
        private IConfigurationRoot _config;
        private Extractor _extractor;
        public BotContext()
        {
            _extractor = new Extractor();
            _awaitingINN = new Dictionary<long, bool>();
        }
        public async Task RunAsync()
        {
            try
            {
                BuildConfig();
            }
            catch
            {
                throw;
            }
            string botToken = _config["TelegramBotToken"];
            string apiFNS = _config["ApiFNS"];

            if (string.IsNullOrEmpty(botToken) || string.IsNullOrEmpty(apiFNS))
            {
                Console.WriteLine("Bot-token didn't find");
                return;
            }
            if (string.IsNullOrEmpty(apiFNS))
            {
                Console.WriteLine("API-key for access didn't find");
                return;
            }

            var botClient = new TelegramBotClient(botToken);

            var offset = 0; 

            while (true)
            {
                var updates = await botClient.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    var message = update.Message;

                    if (message == null) 
                    {
                        continue;
                    }

                    if (message.Text.StartsWith("/start"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Добро пожаловать!");
                    }
                    else if (message.Text.StartsWith("/help"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Доступные команды:\n/start - начать общение с ботом\n/help - вывести справку о доступных командах");
                    }
                    if (message.Text.StartsWith("/inn"))
                    {
                        var chatId = message.Chat.Id;
                        if (_awaitingINN.ContainsKey(chatId) && _awaitingINN[chatId])
                        {
                            continue;
                        }

                        _awaitingINN[chatId] = true;

                        await botClient.SendTextMessageAsync(chatId, "Введите ИНН компании для поиска:");

                        continue;
                    }

                    if (_awaitingINN.ContainsKey(message.Chat.Id) && _awaitingINN[message.Chat.Id])
                    {

                        string inn = message.Text;
                        var result = await _extractor.GetCompanyInfoByINN(inn, apiFNS);
                        await botClient.SendTextMessageAsync(message.Chat.Id, result);

                        _awaitingINN[message.Chat.Id] = false;
                    }

                    offset = update.Id + 1;
                }

                Thread.Sleep(1000);
            }
        }
        private void BuildConfig()
        {
            try
            {
                _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "conf")
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
            catch
            {
                throw;
            }
        }
    }

}
