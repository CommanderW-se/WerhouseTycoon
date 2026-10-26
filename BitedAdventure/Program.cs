using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerhouseTycoon
{
    public class Werhouse
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Fame { get; set; }
        public int Products { get; set; }
        public int Capacity { get; set; }
        public int Production { get; set; }
        public Werhouse(string name) {
            Name = name;
            Money = 0;
            Fame = 0;
            Products = 0;
            Capacity = 20;
            Production = 1;
        }
        public void WerhouseLoad(string name,int money,int fame, int products, int capacity)
        {
            Name = name;
            Money = money;
            Fame = fame;
            Products = products;
            Capacity = capacity;
        }
        public void Print()
        {
            Console.WriteLine($"{Name}, $={Money} *={Fame} {Products}/{Capacity} Generated: {Production}/turn ");
        }
    }
    public class Upgrades
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }

        public Upgrades (string name, string description,string type, int amount)
        {
            Name = name;
            Description = description;
            Type = type;
            Amount = amount;
        }
        public void Print()
        {
            Console.WriteLine("==================");
            Console.WriteLine($"{Name}:");
            Console.WriteLine($"    {Type}");
            Console.WriteLine($"    {Amount}");
            Console.WriteLine($"{Description}");
        }
    }
    internal class Program
    {
        static Werhouse Player {  get; set; }
        static bool Runmain { get; set; }
        static List<Upgrades> Upgrades { get; set; }
        static void LoadUppgrads()
        {
             Upgrades = JsonConvert.DeserializeObject<List<Upgrades>>(File.ReadAllText("upgrades.json"));
        }
        static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("1) Start new");
            Console.WriteLine("2) Load");
            Console.WriteLine("3) Exit");
            Console.WriteLine("========================");
            Console.Write(">: ");
            return Convert.ToInt32(Console.ReadLine());
        }
        static void PauseMenu()
        {
            Console.Clear();
            Console.WriteLine("========================");
            Console.WriteLine("1) Return");
            Console.WriteLine("2) Exit");
            Console.WriteLine("========================");
            Console.Write(">: ");
            int v = Convert.ToInt32(Console.ReadLine());
            switch (v)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        Runmain = false;
                        Console.WriteLine("Exiting.....");
                        Console.ReadLine();
                        break;
                    }
            }
        }
        static void UpgradesMenu()
        {
            bool exit = true;
            while(exit)
            {
                try
                {
                    Console.Clear();
                    List<string> avalable = new List<string>();
                    List<int> which = new List<int>();
                    int indexe = 0;
                    foreach (var upp in Upgrades)
                    {
                        if (!avalable.Contains(upp.Type))
                        {
                            avalable.Add(upp.Type);
                            which.Add(indexe);
                        }
                        indexe++;
                    }
                    foreach (int i in which)
                    {
                        Upgrades[i].Print();
                    }
                    Player.Print();
                    Console.WriteLine();
                    Console.Write(">: ");
                    int v = Convert.ToInt32(Console.ReadLine());
                    switch (v)
                    {
                        case 1:
                            {
                                Player.Production += Upgrades[which[0]].Amount;
                                Upgrades.Remove(Upgrades[which[0]]);
                                break;
                            }
                        case 2:
                            {
                                Player.Production += Upgrades[which[1]].Amount;
                                Upgrades.Remove(Upgrades[which[1]]);
                                break;
                            }
                        case 3:
                            {
                                Player.Production += Upgrades[which[2]].Amount;
                                Upgrades.Remove(Upgrades[which[2]]);
                                break;
                            }
                    }
                    Console.Write("Done?(y/n): ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        exit = false;
                    }
                }
                catch (Exception ex )
                {
                    Console.WriteLine("Try entering a nunber(int).");
                }
                
            }
        }
        static void MainLoop()
        {
            Console.Clear();
            Player.Print();
            Console.WriteLine("======================");
            Console.WriteLine("1) Produce");
            Console.WriteLine("2) Uppgrade");
            Console.WriteLine("3) Sell");
            Console.WriteLine("4) Menu");
            Console.Write(">: ");
            int v = Convert.ToInt32(Console.ReadLine());
            switch (v)
            {
                case 1:
                    {
                        if (Player.Capacity != Player.Products)
                        {
                            if (Player.Capacity >= Player.Products+Player.Production)
                            {
                                Player.Products += Player.Production;
                            }
                            else
                            {
                                Player.Products = Player.Capacity;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        UpgradesMenu();
                        break;
                    }
                case 3:
                    {
                        Console.Clear();
                        Console.WriteLine($"Solled {Player.Products} for {Player.Products*10} $");
                        Console.ReadLine();
                        Player.Money += Player.Products * 10;
                        Player.Products = 0;
                        break;
                    }
                case 4:
                    {
                        PauseMenu();
                        break;
                    }
            }
        }
               
        static void Main(string[] args)
        {
            LoadUppgrads();
           
            int v = MainMenu();
            Runmain = true;
            switch(v)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.Write("Give a name to your Werhouse: ");
                        Player = new Werhouse(Console.ReadLine());
                        while (Runmain)
                        {
                            try
                            {
                                MainLoop();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Try entering a nunber(int).");
                                Console.ReadLine();
                            }
                            
                        }
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Exiting....");
                        Console.ReadLine();
                        break;
                    }
            }
        }
    }
}
