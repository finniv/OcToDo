using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class ShowAllTaskCommand:Command
    {
        protected override string Name => "/showalltask";
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
                var tasklist = new TaskEntity().ShowTask(message.From.Username);
                if (tasklist=="")
                {
                    tasklist = "У вас еще нет тасков";
                }
                await client.SendTextMessageAsync(chatId,
                    tasklist,
                    replyToMessageId: messageId,parseMode:ParseMode.Markdown);
            }
        }
    }
}
