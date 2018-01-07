using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    internal class CreateTeamCommand : Command
    {
        private string TeamName { get; set; }
        private int LeaderId { get; set; }

        protected override string Name => "/createteam";

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

            await client.SendTextMessageAsync(chatId,
                "Введите имя команды",
                replyToMessageId: messageId);
            client.OnMessage += AddName;
        }

        private void FindPeopleId(Message message)
        {
            var peopleId = new PeopleEntity().FindPeopleId(message.From.Username);
            if (peopleId == null)
            {
                Client.SendTextMessageAsync(message.Chat.Id, "Ошибка создания команды",
                    replyToMessageId: message.MessageId);
                return;
            }

            var tmEntity = new TeamEntity().CreateTeam(TeamName, (int) peopleId);
            switch (tmEntity)
            {
                case 1:
                case 2:
                    Client.SendTextMessageAsync(message.Chat.Id, "Команда создана.",
                        replyToMessageId: message.MessageId);
                    break;
                case 0:
                    Client.SendTextMessageAsync(message.Chat.Id, "Ошибка.",
                        replyToMessageId: message.MessageId);
                    break;
                case -1:
                    Client.SendTextMessageAsync(message.Chat.Id, "Ошибка! Команда уже создана.",
                        replyToMessageId: message.MessageId);
                    break;
                default:
                    Client.SendTextMessageAsync(message.Chat.Id, "Команда создана.",
                        replyToMessageId: message.MessageId);
                    break;
            }
        }

        private void AddName(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id, "Этот текст будет установлен как имя комманды",
                    replyToMessageId: e.Message.MessageId);
                TeamName = e.Message.Text;
                FindPeopleId(e.Message);
                Client.OnMessage -= AddName;
                Client = null;
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id, "Не корректно.Введите /createTeam",
                    replyToMessageId: e.Message.MessageId);
                Client.OnMessage -= AddName;
                Client = null;
            }
        }
    }
}
