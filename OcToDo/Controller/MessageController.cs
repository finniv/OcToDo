using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OcToDo.Model;
using OcToDo.Model.Commands;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace OcToDo.Controller
{
    public static class MessageController
    {
        public static async void Update(object sender, MessageEventArgs eventArgs)
        {
            var commands = Bot.Commands;
            var messeage = eventArgs.Message;
            var client = await Bot.GetTask();
            var current = 0;
            foreach (var command in commands)
            {
                var contains = command.Contains(messeage.Text);
                if (contains)
                {
                    command.Execute(messeage, client);
                    break;
                }

                current++;
                if (commands.Count == current)
                {
                    messeage.Text = "/start";
                    var elementAtOrDefault = commands.ElementAtOrDefault(0);
                    elementAtOrDefault?.Execute(messeage, client);
                }
            }
        }
    }
}