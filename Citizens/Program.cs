using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizens
{
    public class Shop
    {
        public Shop() {
            Merch();
        }
        public enum Product
        {
            Feed,
            Milk,
            Meat
        }
        List<Eat> products = new List<Eat>();

        private void Merch()
        {
            products.Add(AddItem("Feed", 5, 3));
            products.Add(AddItem("Milk", 10, 7));
            products.Add(AddItem("Meat", 15, 10));
        }
        private Eat AddItem(string name, int cost, int satiety)
        {
            Eat eat = new Eat();
            eat.Name = name;
            eat.Cost = cost;
            eat.Satiety = satiety;
            return eat;
        }

        public void ShowProduct()
        {
            Merch();
            Console.WriteLine("Product | Cost | Satiety ");
            foreach (Eat e in products)
            {
                Console.WriteLine(e.Name + " | " + e.Cost + " | " + e.Satiety);
            }
        }

        public void BuyEat(Product p, int money, Bag bag, Card card)
        {
            switch (p)
            {
                case Product.Feed:
                    if (money > 5)
                    {
                        Console.WriteLine("+1 Feed");
                        bag.addItem(products[0]);
                        card.money -= 5;
                    }
                    else Console.WriteLine("Not money!");
                    break;
                case Product.Milk:
                    if (money > 10)
                    {
                        Console.WriteLine("+1 Milk");
                        bag.addItem(products[1]);
                        card.money -= 10;
                    }
                    else Console.WriteLine("Not money!");
                    break;
                case Product.Meat:
                    if (money > 15)
                    {
                        Console.WriteLine("+1 Meat");
                        bag.addItem(products[2]);
                        card.money -= 15;
                    }
                    else Console.WriteLine("Not money!");
                    break;
            }
        }
    }

    public class Card
    {
        public Card(int money)
        {
            Random r = new Random();
            string num = r.Next(1, 9) + r.Next(0, 9) + r.Next(0, 9) + "-"
                + r.Next(0, 9) + r.Next(0, 9) + r.Next(0, 9) + "-"
                + r.Next(1, 9) + r.Next(0, 9) + r.Next(0, 9);
            this.number = num;
            this.money = money;
        }
        public void showCard()
        {
            Console.WriteLine("Id: " + number + " || Money: " + money);
        }
        public void setMoney(int m)
        {
            money = m;
        }
        protected string number { get; set; }
        public int money { get; set; }

    }
    public class Bag
    {
        List<Eat> eat = new List<Eat>();

        public void showBag()
        {
            Console.WriteLine("Item | Cost ");
            foreach (Eat e in eat)
            {
                Console.WriteLine(e.Name + " | " + e.Cost);
            }
        }

        public void addItem(Eat e)
        {
            eat.Add(e);
        }
    }
    public class People
    {
        private int money { get; set; }
        string name;
        Dog dog;

        Bag bag = new Bag();
        Card card = new Card(0);

        public People(string name, Dog dog)
        {
            this.name = name;
            this.dog = dog;
            money = 100;
            card.setMoney(money);
        }

        public void showPeople() {
            Console.Write("Name :" + name + Environment.NewLine + "Dog :");
            showDog().Statistic(1);
        }

        public Dog showDog()
        {
            return dog;
        }

        public void showBag()
        {
            bag.showBag();
        }

        public void showCard()
        {
            card.showCard();
        }

        public void buyEat(Shop.Product p, Shop m)
        {
            m.BuyEat(p, money, bag, card);
        }

        public void setMoney(int pass, int m)
        {
            if (pass == 123) money = m;
        }
        public int getMoney(int pass)
        {
            if (pass == 123) return money;
            return 0;
        }
    }
    public class Program
    {
        public static Shop market = new Shop();

        public static List<People> citizen = new List<People>();
        List<Dog> dog = new List<Dog>();

        public static void Start() {
            AgainStart:
            int people = 0;
            Console.WriteLine("What do you want discover?" + Environment.NewLine);
            foreach (People p in citizen)
            {
                Console.Write(Environment.NewLine + "===================" + Environment.NewLine + people + ".");
                p.showPeople();
                people++;
            }
            int key = 0;
            try
            {
                key = Console.Read()-48;
                people = key;
                Again:
                Console.Clear();
                Console.Write(Environment.NewLine + "===================" + Environment.NewLine);
                key = 0;
                citizen[people].showPeople();
                Console.WriteLine(Environment.NewLine + "1.Show bag" + Environment.NewLine + "2.Show dog" +
                    Environment.NewLine + "3.Show card" + Environment.NewLine + "4.Buy eat" + Environment.NewLine + "5.Back");
                //key = Console.Read() - 38;
                //Console.Write(Environment.NewLine + "KEY - " + key);
                Console.ReadLine();
                key = 0;
                key = Console.Read() - 48;
                //Console.Write(Environment.NewLine + "try key - " + key);
                //Console.Read();
                //key = Console.Read() - 48;
                switch (key)
                {
                    case 1:
                        citizen[people].showBag();
                        break;
                    case 2:
                        citizen[people].showDog().Statistic(0);
                        break;
                    case 3:
                        citizen[people].showCard();
                        break;
                    case 4:
                        citizen[people].buyEat(Shop.Product.Feed, market);
                        break;
                    case 5:
                        goto AgainStart;
                        break;
                    default:
                        goto Again;
                        break;
                }
                Console.Write(Environment.NewLine + "Enter to continue... ");
                Console.ReadKey();
                goto Again;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Key:" + key);
                //Start();
            }
            Console.Read();
            //Start();
        }

        public static void Main(string[] args)
        {
            Random rand = new Random();

            string[] name = new string[]
            {"Mol" ,"Alan" ,"Jack" ,
             "Sammy" ,"Toby" ,"Milo",
             "Chance" ,"Bubba" ,"Jackson"};

            citizen.Add(new People("President", new Dog("Ralf", 23)));
            citizen.Add(new People("Pret", new Dog("Raf", 23)));

            Start();

            Console.ReadKey();

            /*
            Shop market = new Shop();
            market.ShowProduct();

            Dog dog = new Dog(name[rand.Next(0, name.Length)], rand.Next(5, 20));
            Dog dog1 = new Dog(name[rand.Next(0, name.Length)], rand.Next(5, 20));

            People Sten = new People("Sten", dog);
            People Kenny = new People("Kenny", dog1);

            Sten.showDog().Statistic();
            Kenny.showDog().Statistic();

            Sten.buyEat(Shop.Product.Feed, market);
            Sten.showBag();
            Sten.showCard();

            Sten.buyEat(Shop.Product.Meat, market);
            Sten.showBag();
            Sten.showCard();
            */

            Console.Read();
        }
    }

    public class Dog
    {
        int speed, eat;
        string name;

        public void Statistic(int a)
        {
            switch (a) {
                case 0: Console.WriteLine("Name: " + name + " Speed: " + speed + " Eat: " + eat);
                    break;
                case 1: Console.Write(name);
                    break;
            }
        }
        public Dog(string name, int speed)
        {
            this.speed = speed;
            this.name = name;
            eat = 100;
        }

        public void Voice()
        {
            eat--;
            Console.WriteLine("Gav!");
        }

        //public void giveEat(int e) {
        //    eat += e;
        //}
    }

    public class Eat
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Satiety { get; set; }
    }
}
