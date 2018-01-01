using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class RegisterCommand : Command
    {
        public override string Name => "register";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var peopleEntity = new PeopleEntity();
            var user = message.From;
            var Username = user.Username;
            var UserId = user.Id;
            await Register(message, client, peopleEntity, Username, UserId);
        }

        private static async System.Threading.Tasks.Task<PeopleEntity> Register(Message message, TelegramBotClient client, PeopleEntity peopleEntity, string Username, int UserId)
        {
            var statusCode = peopleEntity.Register(Username, UserId);
            switch (statusCode)
            {
                case 1:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация успешна");
                    peopleEntity = null;
                    break;
                case 2:
                    await client.SendTextMessageAsync(message.Chat.Id, "Данные обновлены");
                    peopleEntity = null;
                    break;
                case 3:
                    await client.SendTextMessageAsync(message.Chat.Id, "Вы уже в системе зарегистрированы");
                    peopleEntity = null;
                    break;
                case 0:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация не успешна");
                    peopleEntity = null;
                    break;
                default:
                    await client.SendTextMessageAsync(message.Chat.Id, "Регистрация не успешна");
                    peopleEntity = null;
                    break;
            }

            return peopleEntity;
        }
    }
}

