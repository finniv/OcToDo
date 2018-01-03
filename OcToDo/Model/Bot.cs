using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcToDo.Model.Commands;
using Telegram.Bot;

namespace OcToDo.Model
{
    public static class Bot
    {
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static TelegramBotClient Client { get; private set; }

        public static async Task<TelegramBotClient> GetTask()
        {
            if (Client != null)
                return Client;
            _commandsList = new List<Command>
            {
                new StartCommand(),
                new RegisterCommand(),
                new AuthorizeCommand(),
                new CreateTeamCommand()
                //DONE Добавить регистрацию
                //TODO Добавить авторизацию
                //TODO Добавить создание групп
                //TODO Добавить создание тасков
            };

            Client = new TelegramBotClient(BotCore.BotToken);
            await Client.SetWebhookAsync();
            var me = Client.GetMeAsync().Result;
            Console.Title = me.Username;
            return Client;
        }
    }
}
