using OcToDo.Data.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OcToDo.Model.Commands
{
    class UpdateTaskStatusCommand : Command
    {
        protected override string Name => "/updatestatus";
        private int TaskIndex { get; set; }
        private short TaskStatus { get; set; }
        protected override TelegramBotClient Client { get; set; }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            Client = client;
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var plEntity = await UserChecker.CheckPlEntity(message, client, chatId, messageId);
            if (plEntity == null)
            {
                return;
            }
            else
            {
                await Client.SendTextMessageAsync(chatId,
                    "Введите номер задачи",
                    replyToMessageId: messageId);
                var tasklist = new TaskEntity().ShowTask(message.From.Username);
                await Client.SendTextMessageAsync(chatId,
                    tasklist,
                    replyToMessageId: messageId);
                Client.OnMessage += GetTaskIndex;
            }
        }

        private void GetTaskIndex(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {
                try
                {
                    TaskIndex = Convert.ToInt32(e.Message.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= GetTaskIndex;
                    return;
                }

                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Введите статус-код: " +
                    "\n 1. \U00002705 Выполнено" +
                    "\n 2. \U0000274C Не Выполнено" +
                    "\n 3. \U00002B55 В процессе",
                    replyToMessageId: e.Message.MessageId);
                Client.OnMessage -= GetTaskIndex;
                Client.OnMessage += GetStatusCode;
            }
            else
            {
                Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= GetTaskIndex;
        }

        private async void GetStatusCode(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.TextMessage)
            {
                try
                {
                    TaskStatus = Convert.ToInt16(e.Message.Text);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    await Client.SendTextMessageAsync(e.Message.Chat.Id,
                        "Не верное значение",
                        replyToMessageId: e.Message.MessageId);
                    Client.OnMessage -= GetStatusCode;
                    return;
                }
                new TaskEntity().UpdateStatus(TaskIndex,e.Message.From.Username,TaskStatus);
                await Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Статус обновлен",
                    replyToMessageId: e.Message.MessageId);
                var tasklist = new TaskEntity().ShowTask(e.Message.From.Username);
                await Client.SendTextMessageAsync(e.Message.Chat.Id,
                    tasklist,
                    replyToMessageId: e.Message.MessageId);
                Client.OnMessage -= GetStatusCode;

            }
            else
            {
                await Client.SendTextMessageAsync(e.Message.Chat.Id,
                    "Не верное значение",
                    replyToMessageId: e.Message.MessageId);
            }

            Client.OnMessage -= GetStatusCode;
        }
    }
}

