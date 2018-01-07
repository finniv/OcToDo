using System.Threading.Tasks;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class RegisterCommand : Command
    {
        protected override string Name => "/register";
        protected override TelegramBotClient Client { get; set; }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var peopleEntity = new PeopleEntity();
            var user = message.From;
            var username = user.Username;
            var userId = user.Id;
            await Register(message, client, peopleEntity, username, userId);
        }

        private static async System.Threading.Tasks.Task Register(Message message, TelegramBotClient client, PeopleEntity peopleEntity, string username, int userId)
        {
            var statusCode = peopleEntity.Register(username, userId);
            switch (statusCode)
            {
                case 1:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация успешна");
                    break;
                case 2:
                    await client.SendTextMessageAsync(message.Chat.Id, "Данные обновлены");
                    break;
                case -1:
                    await client.SendTextMessageAsync(message.Chat.Id, "Уже зарегистрированы в системе");
                    break;
                case 0:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация не успешна");
                    break;
                default:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация не успешна");
                    break;
            }
        }
    }
}

