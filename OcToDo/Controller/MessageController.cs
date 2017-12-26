using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OcToDo
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
