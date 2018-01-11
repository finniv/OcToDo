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
    class TaskCommand : Command
    {
        protected override string Name => "/addTask";
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
                    "",
                    replyToMessageId: messageId);
            }
        }
    }
}
