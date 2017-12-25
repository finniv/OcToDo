using System.Collections.Generic;
using System.Threading.Tasks;
using OcToDo.Model.Commands;
using Telegram.Bot;


namespace OcToDo.Model
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Commands.Command> _commandsList;

        public static IReadOnlyList<Commands.Command> Commands
        {
            get => _commandsList.AsReadOnly();
        }

        public static async Task<TelegramBotClient> GetTask()
        {
            if (_client !=null)
            {
                return _client;
            }
            _commandsList=new List<Command>();
            _commandsList.Add(new HelloCommand());

            _client = new TelegramBotClient(BotCore.BotToken);
            await _client.SetWebhookAsync("");

            return _client;
        }
    }
}
