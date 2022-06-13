using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination
{
    class Program
    {
        static void Main(string[] args)
        {
            Pagination<Client> pagination = new Pagination<Client>();
            var clients = SetClients(1000);
            pagination.SetPageSize(100);
            pagination.SetData(clients);
            clients = pagination.CurrentPage;
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.PrevPage();
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.NextPage();
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.PrevPage();
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.NextPage();
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            Console.ReadLine();

            pagination.SetPageSize(10);
            clients = pagination.GetData(2);
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.GetData(8);
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.GetData(20);
            Console.WriteLine("\n Новая страница \n");
            PrintClients(clients);
            clients = pagination.GetData(200);
            Console.WriteLine("\n Новая страница \n");
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

    class Pagination<T>
    {
        protected int totalPages = 0;
        protected int currentIndexPage = 1;
        protected int pageSize = 10;
        protected List<T> currentPage;
        private List<T> data;
        public int TotalPages { get { return this.totalPages; } }
        public int CurrentIndexPage { get { return this.currentIndexPage; } }
        public List<T> CurrentPage { get { return this.currentPage; } }

        private void UpdateData()
        {
            if (this.data != null)
            {
               if (this.pageSize > this.data.Count()) this.pageSize = this.data.Count();
                CalculateTotalPages();
                GetCurrentRecords(1);
            }
        }
        public List<T> GetData(int page)
        {
            // Функция служит для получения даннх для определенной страницы

            GetCurrentRecords(page);
            return this.currentPage;
        }
        public void SetData(List<T> data)
        {
            // Метод служаит для установки list, по которому необходимо произвести пагинацию
            this.data = data;
            UpdateData();
        }

        public void SetPageSize(int size)
        {
            this.pageSize = size;
            UpdateData();
        }

        private void CalculateTotalPages()
        {
            // Метод пересчитывает количество страниц
            int rowCount;
            rowCount = Convert.ToInt32(this.data.Count());
            this.totalPages = rowCount / this.pageSize;
            if (rowCount % this.pageSize > 0)
                this.totalPages += 1;
        }

        private void GetCurrentRecords(int page)
        {
            // Метод устанавливает содержание страницы по нужному номеру
            if (page <= 0 || page > TotalPages)
            {
                Console.WriteLine("Bad index...");
                return;
            }

            if (page == 1) this.currentPage = this.data.Take(this.pageSize).ToList();
            else
            {
                int previousPageOffSet = (page - 1) * this.pageSize;
                List<T> excludeData = this.data.Take(previousPageOffSet).ToList();
                this.currentPage = this.data.Where(p => !excludeData.Contains(p)).Take(this.pageSize).ToList();
            }
        }

        public List<T> NextPage()
        {
            if (this.currentIndexPage < TotalPages)
            {
                this.currentIndexPage++;
                GetCurrentRecords(this.currentIndexPage);
            }
            return this.currentPage;
        }
        public List<T> PrevPage()
        {
            if (this.currentIndexPage > 1)
            {
                this.currentIndexPage--;
                GetCurrentRecords(this.currentIndexPage);
            }
            return this.currentPage;
        }



    }
}
