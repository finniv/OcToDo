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

        public string Telegram_ID
        {
            get
            {
                return telegram_ID;
            }

            set
            {
                telegram_ID = value;
            }
        }

        private string telegram_ID;

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            Telegram_ID = message.Chat.Username;
            await client.SendTextMessageAsync(chatId,
                "Введите /setlogin",
                replyToMessageId: messageId);

            client.OnMessage += SetLogin;
        }

        private async void SetLogin(object sender, MessageEventArgs e)
        {
            TelegramBotClient client = await Bot.GetTask();
            PeopleEntity peopleEntity = new PeopleEntity();
            if (peopleEntity.Register(Telegram_ID))
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

