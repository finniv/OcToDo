using System.Threading.Tasks;
using OcToDo.Data.DataBase;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model
{
    static class UserChecker
    {
        public static async Task<int?> CheckPlEntity(Message message, TelegramBotClient client, long chatId, int messageId)
        {
            var plEntity = new PeopleEntity().FindPeopleId(message.From.Username);

            if (plEntity==null)
            {
                await client.SendTextMessageAsync(chatId,
                    "Пройдите регистрацию",
                    replyToMessageId: messageId);
            }
            return plEntity;
        }
    }
}