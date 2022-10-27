using System;
using System.Diagnostics;
using System.Linq.Expressions;

public static class StringExtensions
{
    public static bool ContainsCaseInsensitive(this string source, string substring)
    {
        return source?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;
    }
}
abstract class Payment
{
    public int ammount;
}
class Credit : Payment
{
    public int number;
    public string type;
    public DateTime expDate;
    public Credit(int number, string type, string expDate, int ammount)
    {
        this.number = number;
        this.type = type;
        try
        {
            DateTime expDate_dt = DateTime.ParseExact(expDate, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            this.expDate = expDate_dt;

        }
        catch { throw new("date ERROR"); }
        this.ammount = ammount;

    }
    public override string? ToString()
    {
        return $"Credit: {ammount}";
    }
}
class Cash : Payment
{
    public int cashTendered;

    public Cash(int cashTendered, int ammount)
    {
        this.cashTendered = cashTendered;
        this.ammount = ammount;
    }

    public override string? ToString()
    {
        return $"Ammount: {ammount} Cash:{cashTendered}, Change:{cashTendered-ammount} ";
    }
}
//-----------------------------------------------------------------------------
class Item
{
    public string title;
    public int yearPublished;
    public int price;
    public Item() { }
    public virtual void display()
    {
        Console.WriteLine($"title: {title} \nyearPublished: {yearPublished} \nprice: {price}");
    }
}
class Book : Item
{
    public string author;
    public int edition;
    public Book() { }
    public Book(string title, int yearPublished, int price, string author, int edition)
    {
        this.title = title;
        this.yearPublished = yearPublished;
        this.price = price;
        this.author = author;
        this.edition = edition;
    }
    public override void display()
    {
        Console.WriteLine($"Book\ntitle: {title} \nyearPublished: {yearPublished} \nprice: {price}" +
            $"\nauthor: {author}\nedition: {edition}");
    }
}
class MusicCD : Item
{
    public string artist;
    public MusicCD() { }
    public MusicCD(string title, int yearPublished, int price, string artist)
    {
        this.title = title;
        this.yearPublished = yearPublished;
        this.price = price;
        this.artist = artist;
    }
    public override void display()
    {
        Console.WriteLine($"MusicCD\ntitle: {title} \nyearPublished: {yearPublished} \nprice: {price}" +
            $"\nartist: {artist}");
    }
}
class Software : Item
{
    public string version;
    public Software() { }
    public Software(string title, int yearPublished, int price, string version)
    {
        this.title = title;
        this.yearPublished = yearPublished;
        this.price = price;
        this.version = version;
    }
    public override void display()
    {
        Console.WriteLine($"Software\ntitle: {title} \nyearPublished: {yearPublished} \nprice: {price}" +
            $"\nversion: {version}");
    }
}
abstract class User
{
    private int id;
    private string fullName;
    private string phoneNumber;
    private string address;

    public int Id { get => id; set => id = value; }
    public string FullName { get => fullName; set => fullName = value; }
    public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    public string Address { get => address; set => address = value; }
    public abstract void showInfo();
}
class Manager : User
{
    private int salary;
    public int Salary { get => salary; set => salary = value; }
    public Manager() { }
    public Manager(int id, string fullName, string phoneNumber, string address, int salary)
    {
        this.Id = id;
        this.FullName = fullName;
        this.PhoneNumber = phoneNumber;
        this.Address = address;
        this.Salary = salary;
    }

    public override void showInfo()
    {
        Console.WriteLine($"id: {Id} \nFull Name: {FullName} \nPhone Number: {PhoneNumber}" +
                    $"\nAddress: {Address}\nSalary: {salary}");
    }
    public List<Item> deleteItem(List<Item> itemlist)
    {
        Console.WriteLine("Title of item you want to delete:");
        string title = Console.ReadLine();
        for (int i = 0; i < itemlist.Count; i++)
        {
            if (itemlist[i].title.Equals(title))
            { itemlist.RemoveAt(i); i--;}
        }
        return itemlist;
    }
    public void viewAllItem(List<Item> itemlist)
    {
        Console.WriteLine("LIST OF ALL ITEMS");
        Console.WriteLine("--------------");
        foreach (var item in itemlist)
        {
            item.display();
            Console.WriteLine("--------------");
        }
    }
    public List<Item> addItem(List<Item> itemlist)
    {
        try
        {
            Console.WriteLine("What type of item? (Book,MusicCD,Software)");
            string type = Console.ReadLine();
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Year Published: ");
            int yearPublished = int.Parse(Console.ReadLine());
            Console.WriteLine("Price: ");
            int price = int.Parse(Console.ReadLine());
            if (type.Equals("book", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Author: ");
                string author = Console.ReadLine();
                Console.WriteLine("edition: ");
                int edition = int.Parse(Console.ReadLine());
                itemlist.Add(new Book(title, yearPublished, price, author, edition));
            }
            else if (type.Equals("musiccd", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Artist: ");
                string artist = Console.ReadLine();
                itemlist.Add(new MusicCD(title, yearPublished, price, artist));
            }
            else if (type.Equals("software", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("version: ");
                string version = Console.ReadLine();
                itemlist.Add(new Software(title, yearPublished, price, version));
            }
            else throw new("loi");
        }
        catch { Console.WriteLine("Please Try Again"); }
        return itemlist;
    }
}
class Customer : User
{
    public Customer() { }
    public Customer(int id, string fullName, string phoneNumber, string address)
    {
        this.Id = id;
        this.FullName = fullName;
        this.PhoneNumber = phoneNumber;
        this.Address = address;
    }
    public override void showInfo()
    {
        Console.WriteLine($"id: {Id} \nFull Name: {FullName} \nPhone Number: {PhoneNumber}" +
                    $"\nAddress: {Address}");
    }
    public void searchItemByTitle(List<Item> itemlist)
    {
        Boolean check = false;
        Console.WriteLine("What item are you looking for? ");
        var title = Console.ReadLine();
        foreach (var item in itemlist)
        {
            if (item.title.ContainsCaseInsensitive(title))
            {
                item.display(); check = true;
                Console.WriteLine("--------------");
            }
        }
        if (check==false) Console.WriteLine("Nothing Found");
    }
}

class Order
{
    public DateTime date;
    public List<Item> items = new List<Item>();
    public Customer customer;
    public int total;
    public Payment paymentMethod;
    public Order(string date, Customer customer)
    {
        this.customer = customer;
        DateTime Date_dt = DateTime.ParseExact(date, "dd-MM-yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
        this.date = Date_dt;
    }
    public void addItem(List<Item> itemlist,string title)
    {
        foreach (var item in itemlist)
           if(item.title.Equals(title))
            items.Add(item);
        calcTotal();
    }
    public void calcTotal()
    { int total = 0;
        foreach (var item in items)
        {
            total += item.price;
        }
        this.total= total;
    }
    public void selectPaymentMethod()
    {
        Console.WriteLine($"The amount to be paid is:{total}\n What is your payment Method? (Credit/Cash): ");
        string pm = Console.ReadLine();
        if (pm.Equals("credit", StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine("Card Number:");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Type of your Card:");
            string type = Console.ReadLine();
            Console.WriteLine("Expiration date(dd-MM-yyyy):");
            string expdate = Console.ReadLine();
            paymentMethod = new Credit(number, type, expdate, total);
        }
        else if (pm.Equals("cash", StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine("Cash:");
            int number = int.Parse(Console.ReadLine());
            paymentMethod = new Cash(number, total);
        }
        else throw new("Payment method ERROR");
    }

    public override string? ToString()
    {
        string str = "";
        foreach (var item in items)
        {
            str += item.title+", ";
        }
        return $"Customer name:{customer.FullName}\nitems:{str}\nPayment: {paymentMethod}" ;
    }
}
class Program
{

    static void Main(string[] args)
    {
        List<Item> itemlist = new List<Item>();
        List<Order> orders = new List<Order>();
        Payment credit = new Credit(200, "VISA", "10-12-2022", 300);
        itemlist.Add(new Book("Doraemon", 2002, 100, "Fuji", 1));
        itemlist.Add(new Book("Doraemon 2", 2002, 100, "Fuji", 2));
        itemlist.Add(new Book("Doraemon 3", 2002, 100, "Fuji", 2));
        itemlist.Add(new Software("Photoshop", 2002, 100, "22.1"));
        itemlist.Add(new MusicCD("Love yourself", 2022, 2, "Justin"));
        Console.WriteLine("role: ");
        string role=Console.ReadLine();
        if (role.Equals("manager"))
        {
            Manager manager = new Manager(1001, "Pham Thanh Son", "0762703988", "Da Nang", 2000);
            int x;
            do
            {
                Console.WriteLine("--------------");
                Console.WriteLine($"1:View all items\n2:Add a new item\n3:Delete item\n4:Exit");
                Console.WriteLine("Choose your option:");
                x = int.Parse(Console.ReadLine());
                switch (x)
                {
                    case 1:
                        manager.viewAllItem(itemlist);
                        break;
                    case 2:
                        manager.addItem(itemlist);
                        break;
                    case 3:
                        manager.deleteItem(itemlist);
                        break;
                    default:
                        // code block
                        break;
                }
            } while (x != 4);
        }
        else
        {
            Customer person = new Customer(1, "Ho Duc Tan", "0762703989", "Quang Nam");
            Order order = new Order("20-11-2022", person);
            int x;
            do
            {
                Console.WriteLine("--------------");
                Console.WriteLine($"1:Search item by title\n2:ADD Item to order\n3: Print order detail\n4:Exit");
                Console.WriteLine("Choose your option:");
                x = int.Parse(Console.ReadLine());
                switch (x)
                {
                    case 1:
                        person.searchItemByTitle(itemlist);
                        break;
                    case 2:
                        Console.WriteLine("Title of item you want to add: ");
                        string title = Console.ReadLine();
                        order.addItem(itemlist, title);
                        break;
                    case 3:
                        order.selectPaymentMethod();
                        Console.WriteLine("--------------");
                        Console.WriteLine(order);
                        Console.WriteLine("--------------");
                        break;
                    default:
                        // code block
                        break;
                }
            } while (x != 4);
         }
        //person.searchItemByTitle(itemlist);

        //try
        //{
        //    Order order = new Order("20-11-2022", person);
        //    order.addItem(itemlist[0]);
        //    order.addItem(itemlist[2]);
        //    order.selectPaymentMethod();
        //    Console.WriteLine(order);
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //}
    }
}
