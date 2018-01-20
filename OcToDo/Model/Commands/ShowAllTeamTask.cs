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
    class ShowAllTeamTask:Command
    {
        protected override string Name => "/showAllteamtask";
        protected override TelegramBotClient Client { get; set; }
        private int Index { get; set; }
        private int? TeamId { get; set; }

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

        private async void ChoseTeam(object sender, MessageEventArgs e)
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
                    await Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= ChoseTeam;
                    return;
                }
                TeamId = new TeamEntity().FindTeamIdByIndex(Index, e.Message.From.Username);
                var tasklist = new TaskEntity().ShowTask((int)TeamId);
                if (tasklist == "")
                {
                    tasklist = "У вас еще нет тасков";
                }
                await Client.SendTextMessageAsync(e.Message.Chat.Id,
                    tasklist,
                    replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
            }
            else
            {
                await Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= ChoseTeam;
        }
    }
}
