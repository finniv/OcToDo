using System;

namespace OcToDo
{
    class Program
    {
        static void Main()
        {
            Bot.GetTask();
            Bot.Client.OnMessage += MessageController.Update;
            Bot.Client.StartReceiving();
            Console.ReadLine();
            Bot.Client.StopReceiving();
            //TODO Добавить вывод логов
        }
    }
}
