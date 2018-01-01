using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class Authorize:Command
    {
        public override string Name => "login";

        public string Username { get; private set; }

        public async override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            Username = message.Chat.Username;
            await client.SendTextMessageAsync(chatId,
                "Введите /setlogin",
                replyToMessageId: messageId);
        }
    }
}
