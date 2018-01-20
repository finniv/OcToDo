using OcToDo.Data.DataBase;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class AddTaskCommand : Command
    {
        private int Index { get; set; }
        private int ActivitiesId { get; set; }
        private int? TeamId { get; set; }
        private int TeamContent { get; set; }
        protected override string Name => "/addTask";
        protected override TelegramBotClient Client { get; set; }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            Client = client;
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var plEntity = await UserChecker.CheckPlEntity(message, client, chatId, messageId);
            if (plEntity == null)
            {
                return;
            }
            else
            {
                await Client.SendTextMessageAsync(chatId,
                    "Выберете команду",
                    replyToMessageId: messageId);
                var teamList = new TeamEntity().ShowTeam(message.From.Username);
                await client.SendTextMessageAsync(chatId,
                    teamList,
                    replyToMessageId: messageId);
                client.OnMessage += ChoseTeam;
            }
        }
        private void ChoseTeam(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {
                try
                {
                    Index = Convert.ToInt32(e.Message.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= ChoseTeam;
                    return;
                }

                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Список групп задач");
                ChoseActivities(e.Message);
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= ChoseTeam;
        }

        private async void ChoseActivities(Message message)
        {
            var teamIdByIndex = new TeamEntity().FindTeamIdByIndex(Index, message.From.Username);
            var activitiesList = new ActivitiesEntity().ShowActivities((int)teamIdByIndex);
            await Client.SendTextMessageAsync(message.Chat.Id,
                activitiesList,
                replyToMessageId: message.MessageId);
            await Client.SendTextMessageAsync(message.Chat.Id,
                "Выберете группу задач");
            Client.OnMessage += GetActivitiesId;
        }

        private void GetActivitiesId(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
             TeamId = new TeamEntity().FindTeamIdByIndex(Index, e.Message.From.Username);
            if (e.Message.Type == MessageType.TextMessage)
            {
                try
                {
                    Index = Convert.ToInt32(e.Message.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= GetActivitiesId;
                    return;
                }

                var activitiesByIndex = new ActivitiesEntity().FindActivitiesIdByIndex(Index, (int) TeamId);
                ActivitiesId = (int)activitiesByIndex;
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Введите username участника команды",
                    replyToMessageId: e.Message.MessageId);

                Client.OnMessage -= GetActivitiesId;
                Client.OnMessage += GetInfo;
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }
            Client.OnMessage -= GetActivitiesId;
        }

        private void GetInfo(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {

                if (TeamId != null)
                {
                    var teamContent = new TeamEntity().GetTeamContentId((int)TeamId, e.Message.Text);

                    if (teamContent != null)
                    {
                        TeamContent = (int)teamContent;
                        Client.SendTextMessageAsync(e.Message.Chat.Id,
                            "Введите имя таска",
                            replyToMessageId: e.Message.MessageId);
                        Client.OnMessage += AddTask;
                        Client.OnMessage -= GetInfo;
                    }
                    else
                    {
                        Client.SendTextMessageAsync(e.Message.Chat.Id,
                            "Не верное значение",
                            replyToMessageId: e.Message.MessageId);
                        Client.OnMessage -= GetInfo;
                    }
                }
                else
                {
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= GetInfo;
                }
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }
            Client.OnMessage -= GetInfo;
        }

        private void AddTask(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Этот текст будет установлен как имя таска",
                    replyToMessageId: e.Message.MessageId);
                var addTask= new TaskEntity().AddTask(e.Message.Text,ActivitiesId,TeamContent);
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    addTask > 0 ? "Таск добавлен" : "Таск не добавлен",
                    replyToMessageId: e.Message.MessageId);
                Client.OnMessage -= AddTask;
            }
        }
    }
}
