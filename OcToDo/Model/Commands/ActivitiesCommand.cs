using System;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class ActivitiesCommand:Command
    {
        private int Index;

        protected override string Name => "/addActivities";
        protected override TelegramBotClient Client { get; set; }
        public override async void Execute(Message message, TelegramBotClient client)
        {
            Client = client;
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var plEntity = new PeopleEntity().FindPeopleId(message.From.Username);
            if (plEntity == null)
            {
                await Client.SendTextMessageAsync(chatId,
                    "Пройдите регистрацию",
                    replyToMessageId: messageId);
            }
            else
            {
                await Client.SendTextMessageAsync(chatId,
                    "Введите номер комманды",
                    replyToMessageId: messageId);
                var teamList = new TeamEntity().ShowTeam(message.From.Username);
                await Client.SendTextMessageAsync(chatId,
                    teamList,
                    replyToMessageId: messageId);
                Client.OnMessage += ChoseTeam;
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
                AddActivities(e.Message);
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= ChoseTeam;
        }

        private void AddActivities(Message message)
        {
            Client.SendTextMessageAsync(message.Chat.Id,
                "Введите имя для вашей группы задач",
                replyToMessageId: message.MessageId);
            Client.OnMessage += SetActivities;
        }

        private void SetActivities(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.TextMessage) return;
            var teamIdByIndex = new TeamEntity().FindTeamIdByIndex(Index, e.Message.From.Username);
            if (teamIdByIndex != null)
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    AddActivitiesToTeam(e.Message.Text, teamIdByIndex) > 0 ? "Группа задач добавлена" : "Группа задач не добавлена",
                    replyToMessageId: e.Message.MessageId);
            }
        }

        private int AddActivitiesToTeam(string activitiesName, int? teamIdByIndex)
        {
            if (teamIdByIndex == null ) return 0;
            return new ActivitiesEntity().AddActivities(activitiesName,(int)teamIdByIndex);
        }
    }
}
