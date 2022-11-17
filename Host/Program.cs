using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(kret.Service)))
            {
                host.Open();
                Console.Title = "Сервер";
                Console.WriteLine("Сервер запущен");
                Console.WriteLine("Нажмите любую клавишу, чтобы остановить работу сервера.");
                Console.ReadKey();
            }
        }
    }
}
