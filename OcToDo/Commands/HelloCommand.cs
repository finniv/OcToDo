using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Commands
{
    class HelloCommand : Command
    {
        public override string Name => "hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId,
                "Добро пожаловать в OcToDo, предлагаю вам авторизоваться или зарегистрироваться",
                replyToMessageId: messageId);
        }
        //TODO:Добавить обработку регистрации.
    }
}
