using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OcToDo.Model.Commands
{
    class Authorize:Command
    {
        public override string Name => "login";
        public override void Execute(Message message, TelegramBotClient client)
        {
            
        }
    }
}
