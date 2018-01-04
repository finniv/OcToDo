using System;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class AddToTeamCommand : Command
    {
        protected override string Name => "/addtoteam";
        protected override TelegramBotClient Client { get; set; }
        private int Index { get; set; }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            Client = client;
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId,
                "Введите номер комманды",
                replyToMessageId: messageId);
            var teamList = new TeamEntity().ShowTeam(message.From.Username);
            await client.SendTextMessageAsync(chatId,
                teamList,
                replyToMessageId: messageId);
            client.OnMessage += ChoseTeam;
        }

        private void ChoseTeam(object sender, Telegram.Bot.Args.MessageEventArgs e)
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
                ChosePeople(e.Message);
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= ChoseTeam;
        }

        private void ChosePeople(Message message)
        {
            Client.SendTextMessageAsync(message.Chat.Id,
                "Введите username учасника",
                replyToMessageId: message.MessageId);
            Client.OnMessage += FindPeople;
        }

        private void FindPeople(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.TextMessage) return;
            var peopleId = new PeopleEntity().FindPeopleId(e.Message.Text);
            if (peopleId != null)
            {
                //TODO : Добавить возврат статус кодов, для удобной обработки ошибок
                var teamIdByIndex = new TeamEntity().FindTeamIdByIndex(Index, e.Message.From.Username);
                if (teamIdByIndex != null)
                {
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        AddToTeam(peopleId, teamIdByIndex) > 0 ? "Пользователь добавлен" : "Пользователь не добавлен",
                        replyToMessageId: e.Message.MessageId);
                }
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Пользователь не найден",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= FindPeople;
            Client = null;
        }

        private static sbyte  AddToTeam(int? peopleId, int? teamIdByIndex)
        {
            if (teamIdByIndex == null || peopleId == null) return 0;
           return new TeamEntity().AddToTeam((int) teamIdByIndex, (int) peopleId);
        }
    }
}
