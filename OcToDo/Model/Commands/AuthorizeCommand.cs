using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class AuthorizeCommand:Command
    {
        protected override string Name => "login";
        
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId,
                "Введите /setlogin",
                replyToMessageId: messageId);
        }
    }
}
