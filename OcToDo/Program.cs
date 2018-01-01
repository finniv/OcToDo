using System;
using OcToDo.Controller;
using OcToDo.Model;

namespace OcToDo
{
    internal static class Program
    {
        static void Main()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Bot.GetTask();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Bot.Client.OnMessage += MessageController.Update;
            Bot.Client.StartReceiving();
            Console.ReadLine();
            Bot.Client.StopReceiving();
            //TODO Добавить вывод логов
        }
    }
}
