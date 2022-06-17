using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pagination
{
    
    class Program
    {
        public static void PrintTime()
        {
            DateTime dateTime = new DateTime(2022, 06, 14, 13, 45, 0);
            while (true)
            {
                var dt = dateTime - DateTime.Now;
                var timeSpan = new TimeSpan(dt.Days, dt.Hours, dt.Minutes, dt.Seconds);
                Console.WriteLine(timeSpan.Days + "д. " + timeSpan.Hours + " ч. " + timeSpan.Minutes + " м. " + timeSpan.Seconds + " с.");
                Thread.Sleep(1000);
            }
        }

         static void print()
        {   
            while (true)
            {
                Console.WriteLine("Nice...");
                Thread.Sleep(1000);
            }
        }

        async static Task P()
        {
            await Task.Run(print);
        } 

        async static Task Main(string[] args)
        {

            //Thread thread = new Thread(new ThreadStart(PrintTime));
            //thread.Start();
            //await P();
            //Task task = new Task(print);
            //task.Start();
            Pagination<Client> pagination = new Pagination<Client>();
            var clients = SetClients(1000);
            pagination.SetPageSize(10);
            pagination.SetData(clients);
            clients = pagination.CurrentPage;
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.PrevPage();
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.NextPage();
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.PrevPage();
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.NextPage();
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            Console.ReadLine();
            pagination.SetPageSize(10);
            clients = pagination.GetData(2);
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.GetData(8);
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.GetData(1);
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            clients = pagination.GetData(5);
            Console.WriteLine($"\n Новая страница: {pagination.CurrentIndexPage} из {pagination.TotalPages} \n");
            PrintClients(clients);
            Console.ReadLine();

        }

        public static List<Client> SetClients(int maxEl)
        {
            List<Client> clients = new List<Client>();

            for (int i = 0; i < maxEl; i++)
            {
                clients.Add(new Client(Name: "Artem-" + i.ToString(), Age: (uint)i));
            }
            return clients;
        }

        public static void PrintClients(List<Client> clients)
        {
            if(clients !=null)
                foreach (var item in clients)
                    item.printItem();
        }
    }

    class Client
    {
        public string Name { get; set; }
        public uint Age { get; set; }

        public Client(string Name, uint Age)
        {
            this.Name = Name;
            this.Age = Age;
        }

        public void printItem()
        {
            Console.WriteLine($"Name: {this.Name}\t Age: {this.Age}");
        }
    }

    
}
