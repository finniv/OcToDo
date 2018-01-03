using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    public abstract class Command
    {
        protected abstract string Name { get; }

        public abstract void Execute(Message message, TelegramBotClient client);
        
        public bool Contains(string command)
        {
            if (command == null)
            {
                command = "/start";
            }
            return command.Contains(Name);
        }
    }
}
