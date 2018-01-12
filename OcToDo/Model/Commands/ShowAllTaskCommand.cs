using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

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
            var plEntity = new PeopleEntity().FindPeopleId(message.From.Username);
            if (plEntity == null)
            {
                await client.SendTextMessageAsync(chatId,
                    "Пройдите регистрацию",
                    replyToMessageId: messageId);
                return;
            }
            else
            {
                var tasklist = new TaskEntity().ShowTask(message.From.Username);
                await client.SendTextMessageAsync(chatId,
                    tasklist,
                    replyToMessageId: messageId);
            }
        }
    }
}
