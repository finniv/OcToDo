using System.Threading.Tasks;
using Telegram.Bot;

namespace OcToDo.Model
{
    public static class Bot
    {
        private static TelegramBotClient _client;

        public static async Task<TelegramBotClient> GetTask()
        {
            if (_client !=null)
            {
                return _client;
            }
            _client = new TelegramBotClient(BotCore.BotToken);
            await _client.SetWebhookAsync("");

            return _client;
        }
    }
}
