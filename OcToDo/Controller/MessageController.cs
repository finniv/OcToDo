using OcToDo.Model;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OcToDo.Controller
{
    public static class MessageController
    {
        public static async void Update(object sender, MessageEventArgs eventArgs)
        {
            var commands = Bot.Commands;
            Message messeage = eventArgs.Message;
            var client = await Bot.GetTask();
            foreach (var command in commands)
            {
                if (command.Contains(messeage.Text))
                {
                    command.Execute(messeage, client);
                    break;
                }
            }
        }
    }
}
