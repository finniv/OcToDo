﻿using System;
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
                new CreateTeamCommand(),
                new AddToTeamCommand(),
                new AddActivitiesCommand(),
                new AddTaskCommand(),
                new ShowAllTaskCommand(),
                new UpdateTaskStatusCommand(),
                new DeleteTeamCommand(),
                new ShowAllTeamTask()
                //DONE Добавить регистрацию
                //DONE Добавить авторизацию
                //DONE Добавить создание групп
                //TODO Добавить обработку авторизации
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
