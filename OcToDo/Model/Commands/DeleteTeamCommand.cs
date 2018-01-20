using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class DeleteTeamCommand : Command
    {
        protected override string Name => "/deleteteam";
        protected override TelegramBotClient Client { get; set; }
        private int Index { get; set; }


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
                var teamList = new TeamEntity().ShowTeam(message.From.Username);
                if (teamList == "")
                {
                    teamList = "Вы еще не создали команду";
                }

                else if (teamList!="")
                {
                    await client.SendTextMessageAsync(chatId,
                        "Введите номер комманды",
                        replyToMessageId: messageId);
                }
                await client.SendTextMessageAsync(chatId,
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

                var teamIdByIndex = new TeamEntity().FindTeamIdByIndex(Index, e.Message.From.Username);
                if (teamIdByIndex != null)
                {
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        DeleteTeam(teamIdByIndex) > 0 ? "Команда удалена" : "Команда не удалена",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= ChoseTeam;
                }

                else
                {
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Команда не найдена",
                        replyToMessageId: e.Message.MessageId);
                }
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= ChoseTeam;
        }

        private int DeleteTeam(int? teamIdByIndex)
        {
            if (teamIdByIndex == null ) return 0;
            return new TeamEntity().DeleteTeam((int)teamIdByIndex);
        }
    }
}
