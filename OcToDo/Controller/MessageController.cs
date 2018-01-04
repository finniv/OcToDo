using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OcToDo.Model;
using OcToDo.Model.Commands;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
            if (messeage.Type == MessageType.TextMessage && messeage.Text[0].ToString()=="/")
            {
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

            if (messeage.Type == MessageType.TextMessage)
            {
                Console.WriteLine("Новое сообщение от {0}: {1}",messeage.From.FirstName,messeage.Text);
            }
        }
    }
}