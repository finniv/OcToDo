﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcToDo.Commands;
using Telegram.Bot;

namespace OcToDo
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static TelegramBotClient Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }

        public static async Task<TelegramBotClient> GetTask()
        {
            if (Client != null)
                return Client;
            _commandsList = new List<Command>
            {
                new HelloCommand()
            };

            Client = new TelegramBotClient(BotCore.BotToken);
            await Client.SetWebhookAsync();
            var me = Client.GetMeAsync().Result;
            Console.Title = me.Username;
            
            return Client;
        }
    }
}
