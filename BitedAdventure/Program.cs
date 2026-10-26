using System;
using System.Collections.Generic;
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
        static bool Runmain { get; set; }
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
        static Werhouse MainLoop(Werhouse player)
        {
            Console.Clear();
            player.Print();
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
                        if (player.Capacity != player.Products)
                        {
                            if (player.Capacity >= player.Products+player.Production)
                            {
                                player.Products += player.Production;
                            }
                            else
                            {
                                player.Products = player.Capacity;
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
                        Console.Clear();
                        Console.WriteLine($"Solled {player.Products} for {player.Products*10} $");
                        Console.ReadLine();
                        player.Money += player.Products * 10;
                        player.Products = 0;
                        break;
                    }
                case 4:
                    {
                        PauseMenu();
                        break;
                    }
            }
            return player;
        }
        
        static void Main(string[] args)
        {
            Werhouse Player = new Werhouse("");
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
                                Player = MainLoop(Player);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Try entering a nunber(int).");
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
