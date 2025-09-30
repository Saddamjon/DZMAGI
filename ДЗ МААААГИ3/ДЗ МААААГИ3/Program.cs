using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks3_1_to_3_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 3.1 Child: auto Name, Age>=0, copy ctor
            Child ch1 = new Child { Name = "Mila", Age = 7 };
            Child ch2 = new Child(ch1); // copy
            Console.WriteLine($"Child1: {ch1.Name}, {ch1.Age}; Child2(copy): {ch2.Name}, {ch2.Age}");

            // 3.2 User: auto Name, Age>=0, copy ctor
            User u1 = new User { Name = "Alex", Age = 25 };
            User u2 = new User(u1);
            Console.WriteLine($"User1: {u1.Name}, {u1.Age}; User2(copy): {u2.Name}, {u2.Age}");

            // 3.3 Car: Name, Color, Print; 3 objects in array
            Car car1 = new Car { Name = "Toyota", Color = "White" };
            Car car2 = new Car { Name = "BMW", Color = "Black" };
            Car car3 = new Car { Name = "Audi", Color = "Blue" };
            Car[] cars = { car1, car2, car3 };
            foreach (var c in cars) c.Print();

            // 3.4 Well & GroupWell
            GroupWell gw = new GroupWell(4);
            gw.PrintAllKOS();

            // 3.5 Student with default Name = "Анна"
            Student st = new Student();
            Console.WriteLine($"Student default Name: {st.Name}");

            // 3.6 Numbers: only even can be set through property
            Numbers nums = new Numbers();
            nums.EvenNumber = 7;   // ignored (odd)
            Console.WriteLine($"Numbers after 7 -> {nums.EvenNumber}");
            nums.EvenNumber = 10;  // accepted (even)
            Console.WriteLine($"Numbers after 10 -> {nums.EvenNumber}");

            // 3.7 Person: Age public get, private set; validation 18..130; assigns to private maxAge
            Person p = new Person();
            p.SetAge(20); // allowed
            Console.WriteLine($"Person age: {p.Age}");
            p.SetAge(140); // ignored
            Console.WriteLine($"Person age after 140 attempt: {p.Age}");

            // 3.8 Food: Name, Weight, Category
            Food food = new Food { Name = "Яблоко", Weight = 0.08, Category = "Фрукты" };
            Console.WriteLine($"Food: {food.Name}, {food.Weight} кг, {food.Category}");

            // 3.9 House: Floors(1..16), Flats(1..300), copy ctor
            House house1 = new House(9, 180);
            House house2 = new House(house1); // copy
            Console.WriteLine($"House1: {house1.Floors} floors, {house1.Flats} flats");
            Console.WriteLine($"House2(copy): {house2.Floors} floors, {house2.Flats} flats");

            // 3.10 Animal: private fields name, age; public props Name & Age (Имя/Возраст)
            Animal animal = new Animal { Name = "Барсик", Age = 3 };
            Console.WriteLine($"Animal: {animal.Name}, {animal.Age}");

            // 3.11 Rectangle: Area read-only
            Rectangle rect = new Rectangle(5.0, 3.5);
            Console.WriteLine($"Rectangle area: {rect.Area}");

            // 3.12 Employee: Salary with positivity check
            Employee emp = new Employee();
            emp.Salary = 100000;  // ok
            Console.WriteLine($"Employee salary: {emp.Salary}");
            emp.Salary = -1;      // ignored
            Console.WriteLine($"Employee salary after -1: {emp.Salary}");

            // 3.13 Product: Name/Price props
            Product prod = new Product { Name = "Laptop", Price = 1999.99m };
            Console.WriteLine($"Product: {prod.Name}, {prod.Price:C}");

            // 3.14 Square: Perimeter read-only
            Square sq = new Square(2.5);
            Console.WriteLine($"Square perimeter: {sq.Perimeter}");
        }
    }

    // 3.1
    public class Child
    {
        public string Name { get; set; } // авто-свойство
        private int _age;
        public int Age            // открытое свойство с проверкой >= 0
        {
            get => _age;
            set => _age = value < 0 ? 0 : value;
        }
        public Child() { }
        public Child(Child other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Name = other.Name;
            Age = other.Age;
        }
    }

    // 3.2
    public class User
    {
        public string Name { get; set; }
        private int _age;
        public int Age
        {
            get => _age;
            set => _age = value < 0 ? 0 : value;
        }
        public User() { }
        public User(User other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Name = other.Name;
            Age = other.Age;
        }
    }

    // 3.3
    public class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public void Print() => Console.WriteLine($"Car: {Name}, Color: {Color}");
    }

    // 3.4
    public class Well
    {
        public double KOS { get; private set; } // открытое чтение, ограниченная запись
        public Well(double kos)
        {
            if (kos < 0 || kos > 1) throw new ArgumentOutOfRangeException(nameof(kos), "KOS must be in [0,1].");
            KOS = kos;
        }
    }

    public class GroupWell
    {
        public List<Well> Wells { get; } = new List<Well>();

        public GroupWell(int count)
        {
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be > 0.");

            // Выпуклая комбинация: веса >= 0, сумма = 1
            // Возьмём веса пропорционально i+1 и нормируем.
            double[] raw = Enumerable.Range(1, count).Select(i => (double)i).ToArray();
            double sum = raw.Sum();
            for (int i = 0; i < count; i++)
            {
                double weight = raw[i] / sum; // каждая KOS в [0,1], сумма = 1
                Wells.Add(new Well(weight));
            }

            // (Проверка: сумма близка к 1)
            double total = Wells.Sum(w => w.KOS);
            if (Math.Abs(total - 1.0) > 1e-9)
                throw new InvalidOperationException("KOS weights do not sum to 1.");
        }

        public void PrintAllKOS()
        {
            Console.WriteLine("GroupWell KOS values:");
            for (int i = 0; i < Wells.Count; i++)
                Console.WriteLine($"  Well #{i + 1}: {Wells[i].KOS:F6}");
        }
    }

    // 3.5
    public class Student
    {
        public string Name { get; set; } = "Анна";
    }

    // 3.6
    public class Numbers
    {
        private int num; // внутренняя переменная
        public int EvenNumber
        {
            get => num;
            set
            {
                if (value % 2 == 0)
                    num = value; // только чётные присваиваем
                // нечётные — игнорируем
            }
        }
    }

    // 3.7
    public class Person
    {
        private int maxAge { get; set; } // приватное свойство
        public int Age { get; private set; } // публичное, get внешне; set — только внутри класса

        public void SetAge(int value)
        {
            if (value >= 18 && value <= 130)
            {
                maxAge = value;
                Age = value;
            }
            // иначе — игнор
        }
    }

    // 3.8
    public class Food
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }
    }

    // 3.9
    public class House
    {
        private int floors;
        private int flats;

        public int Floors
        {
            get => floors;
            set
            {
                if (value < 1 || value > 16) throw new ArgumentOutOfRangeException(nameof(Floors), "Floors must be 1..16.");
                floors = value;
            }
        }
        public int Flats
        {
            get => flats;
            set
            {
                if (value < 1 || value > 300) throw new ArgumentOutOfRangeException(nameof(Flats), "Flats must be 1..300.");
                flats = value;
            }
        }

        public House(int floors, int flats)
        {
            Floors = floors;
            Flats = flats;
        }

        // copy ctor
        public House(House other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Floors = other.Floors;
            Flats = other.Flats;
        }
    }

    // 3.10
    public class Animal
    {
        private string name;
        private int age;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Age
        {
            get => age;
            set => age = value;
        }
    }

    // 3.11
    public class Rectangle
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public double Area => length * width; // только чтение
    }

    // 3.12
    public class Employee
    {
        private int salary;
        public int Salary
        {
            get => salary;
            set
            {
                if (value > 0)
                    salary = value;
                // иначе — игнор
            }
        }
    }

    // 3.13
    public class Product
    {
        private string name;
        private decimal price;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public decimal Price
        {
            get => price;
            set => price = value;
        }
    }

    // 3.14
    public class Square
    {
        private double side;
        public Square(double side) { this.side = side; }
        public double Perimeter => 4 * side; // только чтение
    }
}
