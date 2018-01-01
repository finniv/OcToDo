using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class StartCommand : Command
    {
        public override string Name => "start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId,
                "Добро пожаловать в OcToDo, предлагаю вам авторизоваться или зарегистрироваться для этого введите /register или /login",
                replyToMessageId: messageId);
        }
        //TODO:Добавить обработку регистрации.
    }
}
