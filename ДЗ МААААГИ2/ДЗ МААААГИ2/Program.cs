using System;

namespace Tasks2_1_to_2_16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 2.1
            Phone phone1 = new Phone();

            // 2.2
            Computer comp1 = new Computer();

            // 2.3
            Table table = new Table("Dining Table", "Brown");

            // 2.4
            Phone phone2 = new Phone("iPhone", "Black", "14 Pro");
            Phone phone3 = new Phone("Samsung", "Blue", "S23");

            // 2.5
            Car car = new Car();

            // 2.6
            Series series = new Series("Breaking Bad", 5);

            // 2.7
            Cat cat1 = new Cat("Барсик");
            Cat cat2 = new Cat(cat1); // copy constructor

            // 2.8
            Dog dog1 = new Dog();
            Dog dog2 = new Dog("Buddy", 3, "Golden Retriever");

            // 2.9 (использую данные КФУ в общем виде)
            KFU kfu1 = new KFU();
            KFU kfu2 = new KFU("Kazan Federal University", "Kazan, Russia");

            // 2.10
            Laptop laptop = new Laptop("Lenovo", "ThinkPad X1 Carbon", 1800.00m);

            // 2.11
            Fruit fruit1 = new Fruit("Apple", "Red");
            Fruit fruit2 = new Fruit("Banana", "Yellow");

            // 2.12
            Address addr = new Address("Kazan", "Kremlyovskaya", "420008");

            // 2.13
            Rectangle rect = new Rectangle(10.5, 7.2);

            // 2.14
            CoffeeCup cup = new CoffeeCup("Ceramic", "White", "300ml");

            // 2.15
            Employee emp1 = new Employee("Alice", "Manager", 120000);
            Employee emp2 = new Employee("Bob", "Engineer", 150000);

            // 2.16
            Smartphone sp = new Smartphone("Google", "Pixel 8", 256);

            // Небольшие выводы для наглядности
            Console.WriteLine("Tasks 2.1–2.16 done.");
            Console.WriteLine($"Table created: {table.GetInfo()}");
            Console.WriteLine($"Series: {series.GetInfo()}");
            Console.WriteLine($"Cat1: {cat1.Name}, Cat2(copy): {cat2.Name}");
            Console.WriteLine($"Dog2: {dog2.GetInfo()}");
            Console.WriteLine($"KFU2: {kfu2.GetInfo()}");
            Console.WriteLine($"Laptop: {laptop.GetInfo()}");
            Console.WriteLine($"Fruits: {fruit1.GetInfo()} | {fruit2.GetInfo()}");
            Console.WriteLine($"Address: {addr.GetInfo()}");
            Console.WriteLine($"Rectangle: {rect.GetInfo()}");
            Console.WriteLine($"CoffeeCup: {cup.GetInfo()}");
            Console.WriteLine($"Employee1: {emp1.GetInfo()}");
            Console.WriteLine($"Smartphone: {sp.GetInfo()}");
        }
    }

    // 2.1
    public class Phone
    {
        private string name;
        private string color;
        private string model;

        // standard ctor
        public Phone() { }

        // 2.4 param ctor
        public Phone(string name, string color, string model)
        {
            this.name = name;
            this.color = color;
            this.model = model;
        }
    }

    // 2.2
    public class Computer
    {
        public Computer() { } // standard ctor
    }

    // 2.3
    public class Table
    {
        private string name;
        private string color;

        public Table(string name, string color)
        {
            this.name = name;
            this.color = color;
        }

        public string GetInfo() => $"{name} ({color})";
    }

    // 2.5
    public class Car
    {
        public Car() { } // standard ctor
    }

    // 2.6
    public class Series
    {
        private string name;
        private int numOfS;

        public Series(string name, int numOfS)
        {
            this.name = name;
            this.numOfS = numOfS;
        }

        public string GetInfo() => $"{name}, seasons: {numOfS}";
    }

    // 2.7
    public class Cat
    {
        public string Name; // open access

        public Cat(string name)
        {
            Name = name;
        }

        // copy ctor
        public Cat(Cat other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Name = other.Name;
        }
    }

    // 2.8
    public class Dog
    {
        private string name;
        private int age;
        private string breed;

        public Dog() { } // standard

        public Dog(string name, int age, string breed)
        {
            this.name = name;
            this.age = age;
            this.breed = breed;
        }

        public string GetInfo() => $"{name}, {age} y/o, {breed}";
    }

    // 2.9
    public class KFU
    {
        private string NameOfInstitute;
        private string Address;

        // standard: присваиваем некоторые значения
        public KFU()
        {
            NameOfInstitute = "Kazan Federal University";
            Address = "Kazan, Russia";
        }

        public KFU(string nameofi, string address)
        {
            NameOfInstitute = nameofi;
            Address = address;
        }

        public string GetInfo() => $"{NameOfInstitute} — {Address}";
    }

    // 2.10
    public class Laptop
    {
        private string brand;
        private string model;
        private decimal price;

        public Laptop(string brand, string model, decimal price)
        {
            this.brand = brand;
            this.model = model;
            this.price = price;
        }

        public string GetInfo() => $"{brand} {model}, {price:C}";
    }

    // 2.11
    public class Fruit
    {
        private string name;
        private string color;

        public Fruit(string name, string color)
        {
            this.name = name;
            this.color = color;
        }

        public string GetInfo() => $"{name} ({color})";
    }

    // 2.12
    public class Address
    {
        private string city;
        private string street;
        private string zip_code;

        public Address(string city, string street, string zip_code)
        {
            this.city = city;
            this.street = street;
            this.zip_code = zip_code;
        }

        public string GetInfo() => $"{street}, {city}, {zip_code}";
    }

    // 2.13
    public class Rectangle
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public string GetInfo() => $"length={length}, width={width}";
    }

    // 2.14
    public class CoffeeCup
    {
        private string material;
        private string color;
        private string size;

        public CoffeeCup(string material, string color, string size)
        {
            this.material = material;
            this.color = color;
            this.size = size;
        }

        public string GetInfo() => $"{material}, {color}, {size}";
    }

    // 2.15
    public class Employee
    {
        private string name;
        private string position;
        private int salary;

        public Employee(string name, string position, int salary)
        {
            this.name = name;
            this.position = position;
            this.salary = salary;
        }

        public string GetInfo() => $"{name}, {position}, {salary}";
    }

    // 2.16
    public class Smartphone
    {
        private string brand;
        private string model;
        private int storage;

        public Smartphone(string brand, string model, int storage)
        {
            this.brand = brand;
            this.model = model;
            this.storage = storage;
        }

        public string GetInfo() => $"{brand} {model}, {storage} GB";
    }
}
