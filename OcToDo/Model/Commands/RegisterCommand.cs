using System;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class RegisterCommand : Command
    {
        public override string Name => "register";

        public string Username { get; private set; }
        public int UserId   { get; private set; }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            User user = message.From;
            Username = user.Username;
            UserId = user.Id;
            await client.SendTextMessageAsync(chatId,
                "Введите /setlogin",
                replyToMessageId: messageId);

            client.OnMessage += SetLogin;
        }

        private async void SetLogin(object sender, MessageEventArgs e)
        {
            TelegramBotClient client = await Bot.GetTask();
            PeopleEntity peopleEntity = new PeopleEntity();
            if (peopleEntity.Register(Username,UserId))
            {
                

                await client.SendTextMessageAsync(e.Message.Chat.Id, "Регистрация успешна");
            }
            else
            {
                await client.SendTextMessageAsync(e.Message.Chat.Id, "Регистрация не успешна");
            }
        }
    }
}

