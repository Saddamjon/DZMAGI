using System;
using System.Collections.Generic;

namespace Tasks5_All
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ===== 5.1 People + Child =====
            People p = new People { Name = "Ivan", LastName = "Petrov" };
            People.Child ch = new People.Child { Name = "Mila", LastName = "Petrova" };
            Console.WriteLine($"5.1 People: {p.Name} {p.LastName}; Child: {ch.Name} {ch.LastName}");

            // ===== 5.2 Person -> Employee =====
            Person basePerson = new Person("Alex");
            Employee empFromPerson = new Employee("Bob", "Contoso");
            Console.WriteLine($"5.2 Person: {basePerson.Name}; Employee: {empFromPerson.Name} @ {empFromPerson.CompanyName}");

            // ===== 5.3 Programmer (abstract) -> FrontEnd/BackEnd =====
            FrontEndProgrammer fe = new FrontEndProgrammer { Name = "Ann", LastName = "Sokolova" };
            BackEndProgrammer be = new BackEndProgrammer { Name = "Max", LastName = "Orlov" };
            Console.WriteLine("5.3 FE langs: " + string.Join(", ", fe.GetLanguages()));
            Console.WriteLine("    BE langs: " + string.Join(", ", be.GetLanguages()));

            // ===== 5.4 int copy =====
            int a = 5;
            int b = a;     // копируем значение
            b = 9;         // меняем ТОЛЬКО b
            Console.WriteLine($"5.4 a={a}, b={b}  (оба — значимые типы, НЕ ссылочные)");

            // ===== 5.5 Person reference assignment =====
            Person p1 = new Person("Айрат");
            Person p2 = p1;               // та же ссылка
            p2.Name = "Мамай";
            Console.WriteLine($"5.5 p1.Name={p1.Name} (оба — ссылочные, указывают на один объект)");

            // ===== 5.6 Animal -> Dog =====
            Dog d = new Dog { Species = "Canis lupus familiaris", Breed = "Labrador" };
            Console.WriteLine($"5.6 Dog: {d.Species}, {d.Breed}");

            // ===== 5.7 Vehicle -> Car =====
            Car car = new Car { Make = "Toyota", Model = "Corolla", Year = 2020 };
            Console.WriteLine($"5.7 Car: {car.Make} {car.Model} ({car.Year})");

            // ===== 5.8 Employee abstract -> Developer/HR/Manager =====
            Developer dev = new Developer("Ilya", "Smirnov", "M", "Senior Developer") { MainLanguage = "C#" };
            HR hr = new HR("Lena", "Ivanova", "F", "HR Specialist") { Regions = new List<string> { "EU", "CIS" } };
            Manager mgr = new Manager("Petr", "Sidorov", "M", "Project Manager") { TeamSize = 8 };
            Console.WriteLine($"5.8 Dev: {dev.Name} {dev.LastName}, title: {dev.GetTitle()}, lang: {dev.MainLanguage}");
            Console.WriteLine($"    HR : {hr.Name} {hr.LastName}, title: {hr.GetTitle()}, regions: {string.Join("/", hr.Regions)}");
            Console.WriteLine($"    Mgr: {mgr.Name} {mgr.LastName}, title: {mgr.GetTitle()}, team: {mgr.TeamSize}");

            // ===== 5.9 Fruit reference assignment =====
            Fruit f1 = new Fruit("Банан");
            Fruit f2 = f1;            // та же ссылка
            f2.Name = "Груша";
            Console.WriteLine($"5.9 f1.Name={f1.Name} (оба — ссылочные, один объект)");

            // ===== 5.10 string copy =====
            string s1 = "hello";
            string s2 = s1;           // копируется ссылка на неизменяемую строку
            s2 = "bye";               // теперь s2 указывает на другую строку
            Console.WriteLine($"5.10 s1=\"{s1}\", s2=\"{s2}\"  (string — ссылочный, но неизменяемый)");

            // ===================== Наследование =====================
            // 1. Shape -> Circle/Rectangle (Area)
            Shape sh1 = new Circle(3.0);
            Shape sh2 = new RectangleShape(4.0, 2.5);
            Console.WriteLine($"N1 Circle area = {sh1.Area():0.###}; Rectangle area = {sh2.Area():0.###}");

            // 2. Shape.PrintInfo() override -> Triangle/Square
            Shape t = new Triangle(3, 4, 5);
            Shape sq = new Square(3);
            Console.Write("N2 "); t.PrintInfo();
            Console.Write("   "); sq.PrintInfo();

            // 3. Animal base -> Dog/Cat with methods
            Dog2 dog2 = new Dog2("Шарик", 4); dog2.Bark();
            Cat2 cat2 = new Cat2("Мурка", 2); cat2.Meow();

            // 4. Product -> Food/Electronics
            Product pr1 = new FoodProduct("Яблоко", 0.5m, DateTime.Today.AddDays(7));
            Product pr2 = new ElectronicsProduct("Смартфон", 500m, TimeSpan.FromDays(365));
            Console.WriteLine($"N4 Food: {pr1.Info()}");
            Console.WriteLine($"   Electronics: {pr2.Info()}");

            // 5. GeometricFigure -> Circle/Rectangle (CalculateArea)
            GeometricFigure gf1 = new GeoCircle(2.0);
            GeometricFigure gf2 = new GeoRectangle(2.0, 5.0);
            Console.WriteLine($"N5 Areas: circle={gf1.CalculateArea():0.###}, rect={gf2.CalculateArea():0.###}");

            // 6. Account -> Savings/Current with Withdraw (fees)
            SavingsAccount sa = new SavingsAccount("S-001", 1000m, 0.01m);
            CurrentAccount ca = new CurrentAccount("C-001", 1000m, 10m);
            sa.Withdraw(100m); ca.Withdraw(100m);
            Console.WriteLine($"N6 Savings balance={sa.Balance:0.##}; Current balance={ca.Balance:0.##}");

            // ===================== Преобразования типов =====================
            Console.WriteLine($"T1 20°C -> {TemperatureConv.CelsiusToFahrenheit(20):0.##}°F");
            Console.WriteLine($"T2 250 cm -> {LengthConverter.CentimetersToMeters(250):0.##} m");
            Console.WriteLine($"T3 100 USD @ 0.9 -> {CurrencyConv.Convert(100m, 0.9m):0.##}");
            Console.WriteLine($"T4 2.3 kg -> {WeightConverter.KgToGrams(2.3):0} g");
            Console.WriteLine($"T5 10 km -> {DistanceConverter.KmToMiles(10):0.###} mi");
            Console.WriteLine($"T6 3.5 h -> {TimeConverter.HoursToMinutes(3.5):0} min");

            // ===================== Значимые и ссылочные типы =====================
            // 1. struct PointVal
            PointVal pv = new PointVal(1, 2);
            pv.Move(3, -1); // изменится копия в переменной pv
            Console.WriteLine($"V1 Point after move: ({pv.X},{pv.Y})  (struct — значимый, меняется копия)");

            // 2. struct RangeVal intersect
            RangeVal r1 = new RangeVal(0, 5), r2 = new RangeVal(3, 10);
            Console.WriteLine($"V2 Ranges intersect? {r1.Intersects(r2)}  (значимые типы — компактность/локальность)");

            // 3. StudentRef with Grade class + validation
            StudentRef sr = new StudentRef("Nina");
            sr.AssignGrade(new Grade(95));     // ок
            // sr.AssignGrade(new Grade(150)); // бросит исключение
            Console.WriteLine($"V3 {sr.Name} grade={sr.Grade.Value}");

            // 4. struct TimeVal add minutes
            TimeVal tv = new TimeVal(1, 50);
            tv = tv.AddMinutes(15); // получить новое значение (struct — неизменяемая копия)
            Console.WriteLine($"V4 TimeVal = {tv.Hours:D2}:{tv.Minutes:D2}");

            // 5. BookRef info
            BookRef br = new BookRef("War and Peace", "Leo Tolstoy");
            Console.WriteLine($"V5 {br.Info()}");

            // 6. struct RectVal area
            RectVal rv = new RectVal(3, 4);
            Console.WriteLine($"V6 Area = {rv.Area()}");
        }
    }

    // ===== 5.1 =====
    public class People
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public People() { }
        public class Child : People
        {
            public Child() { }
        }
    }

    // ===== 5.2 =====
    public class Person
    {
        public string Name { get; set; }
        public Person() { }
        public Person(string name) { Name = name; }
    }
    public class Employee : Person
    {
        public string CompanyName { get; set; }
        public Employee(string name, string companyName)
        {
            Name = name;
            CompanyName = companyName;
        }
    }

    // ===== 5.3 =====
    public abstract class Programmer
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        protected List<string> Languages; // защищённое поле
        public IEnumerable<string> GetLanguages() { return Languages; }
    }
    public class FrontEndProgrammer : Programmer
    {
        public FrontEndProgrammer()
        {
            Languages = new List<string> { "JavaScript", "TypeScript", "Blazor" };
        }
    }
    public class BackEndProgrammer : Programmer
    {
        public BackEndProgrammer()
        {
            Languages = new List<string> { "C#", "Java" };
        }
    }

    // ===== 5.6 =====
    public class Animal
    {
        public string Species { get; set; }
    }
    public class Dog : Animal
    {
        public string Breed { get; set; }
    }

    // ===== 5.7 =====
    public class Vehicle
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }
    public class Car : Vehicle
    {
        public int Year { get; set; }
    }

    // ===== 5.8 =====
    public abstract class EmployeeBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        protected string Title; // должность
        public string GetTitle() { return Title; }
        protected EmployeeBase(string name, string lastName, string gender, string title)
        {
            Name = name; LastName = lastName; Gender = gender; Title = title;
        }
    }
    public class Developer : EmployeeBase
    {
        public string MainLanguage { get; set; }
        public Developer(string n, string l, string g, string t) : base(n, l, g, t) { }
        public void WriteCode() { /* ... */ }
    }
    public class HR : EmployeeBase
    {
        public List<string> Regions { get; set; } = new List<string>();
        public HR(string n, string l, string g, string t) : base(n, l, g, t) { }
        public void Recruit() { /* ... */ }
    }
    public class Manager : EmployeeBase
    {
        public int TeamSize { get; set; }
        public Manager(string n, string l, string g, string t) : base(n, l, g, t) { }
        public void Plan() { /* ... */ }
    }

    // ===== 5.9 =====
    public class Fruit
    {
        public string Name { get; set; }
        public Fruit(string name) { Name = name; }
    }

    // ===================== Наследование =====================
    // 1. Shape
    public abstract class Shape
    {
        public abstract double Area();
        public virtual void PrintInfo() { Console.WriteLine("Shape"); }
    }
    public class Circle : Shape
    {
        public double R { get; }
        public Circle(double r) { R = r; }
        public override double Area() { return Math.PI * R * R; }
        public override void PrintInfo() { Console.WriteLine($"Circle R={R}, Area={Area():0.###}"); }
    }
    public class RectangleShape : Shape
    {
        public double W { get; }
        public double H { get; }
        public RectangleShape(double w, double h) { W = w; H = h; }
        public override double Area() { return W * H; }
        public override void PrintInfo() { Console.WriteLine($"Rectangle {W}x{H}, Area={Area():0.###}"); }
    }

    // 2. Triangle / Square override PrintInfo
    public class Triangle : Shape
    {
        public double A, B, C;
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Invalid triangle");
            A = a; B = b; C = c;
        }
        public override double Area()
        {
            double s = (A + B + C) / 2;
            return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Triangle sides=({A},{B},{C}), Area={Area():0.###}");
        }
    }
    public class Square : Shape
    {
        public double Side;
        public Square(double side) { Side = side; }
        public override double Area() { return Side * Side; }
        public override void PrintInfo() { Console.WriteLine($"Square side={Side}, Area={Area():0.###}"); }
    }

    // 3. Animal -> Dog2/Cat2 with methods
    public class Animal2
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Animal2(string name, int age) { Name = name; Age = age; }
    }
    public class Dog2 : Animal2
    {
        public Dog2(string n, int a) : base(n, a) { }
        public void Bark() { Console.WriteLine($"N3 Dog {Name}: Гав!"); }
    }
    public class Cat2 : Animal2
    {
        public Cat2(string n, int a) : base(n, a) { }
        public void Meow() { Console.WriteLine($"N3 Cat {Name}: Мяу!"); }
    }

    // 4. Product hierarchy
    public abstract class Product
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        protected Product(string title, decimal price) { Title = title; Price = price; }
        public abstract string Info();
    }
    public class FoodProduct : Product
    {
        public DateTime ExpiryDate { get; set; }
        public FoodProduct(string t, decimal p, DateTime exp) : base(t, p) { ExpiryDate = exp; }
        public override string Info() { return $"{Title}, {Price:C}, годен до {ExpiryDate:yyyy-MM-dd}"; }
    }
    public class ElectronicsProduct : Product
    {
        public TimeSpan Warranty { get; set; }
        public ElectronicsProduct(string t, decimal p, TimeSpan w) : base(t, p) { Warranty = w; }
        public override string Info() { return $"{Title}, {Price:C}, гарантия {Warranty.Days} дн."; }
    }

    // 5. GeometricFigure
    public abstract class GeometricFigure
    {
        public abstract double CalculateArea();
    }
    public class GeoCircle : GeometricFigure
    {
        public double R;
        public GeoCircle(double r) { R = r; }
        public override double CalculateArea() { return Math.PI * R * R; }
    }
    public class GeoRectangle : GeometricFigure
    {
        public double W, H;
        public GeoRectangle(double w, double h) { W = w; H = h; }
        public override double CalculateArea() { return W * H; }
    }

    // 6. Accounts hierarchy
    public abstract class Account
    {
        public string Number { get; }
        public decimal Balance { get; protected set; }
        protected Account(string number, decimal balance) { Number = number; Balance = balance; }
        public abstract void Withdraw(decimal amount);
    }
    public class SavingsAccount : Account
    {
        public decimal FeeRate; // напр., 1% от суммы
        public SavingsAccount(string no, decimal bal, decimal feeRate) : base(no, bal) { FeeRate = feeRate; }
        public override void Withdraw(decimal amount)
        {
            decimal fee = amount * FeeRate;
            if (amount + fee <= Balance) Balance -= (amount + fee);
        }
    }
    public class CurrentAccount : Account
    {
        public decimal FixedFee;
        public CurrentAccount(string no, decimal bal, decimal fee) : base(no, bal) { FixedFee = fee; }
        public override void Withdraw(decimal amount)
        {
            decimal total = amount + FixedFee;
            if (total <= Balance) Balance -= total;
        }
    }

    // ===================== Преобразования типов =====================
    public static class TemperatureConv
    {
        public static double CelsiusToFahrenheit(double c) { return c * 9.0 / 5.0 + 32; }
    }
    public static class LengthConverter
    {
        public static double CentimetersToMeters(double cm) { return cm / 100.0; }
    }
    public static class CurrencyConv
    {
        public static decimal Convert(decimal amount, decimal rate) { return amount * rate; }
    }
    public static class WeightConverter
    {
        public static double KgToGrams(double kg) { return kg * 1000.0; }
    }
    public static class DistanceConverter
    {
        public static double KmToMiles(double km) { return km * 0.621371; }
    }
    public static class TimeConverter
    {
        public static double HoursToMinutes(double hours) { return hours * 60.0; }
    }

    // ===================== Значимые и ссылочные типы =====================
    // 1. struct Point
    public struct PointVal
    {
        public int X, Y;
        public PointVal(int x, int y) { X = x; Y = y; }
        public void Move(int dx, int dy) { X += dx; Y += dy; }
    }

    // 2. struct Range
    public struct RangeVal
    {
        public int Start, End;
        public RangeVal(int s, int e) { Start = s; End = e; }
        public bool Intersects(RangeVal other)
        {
            return !(End < other.Start || other.End < Start);
        }
    }

    // 3. Student with reference Grade (and validation)
    public class Grade
    {
        private int _value;
        public int Value { get { return _value; } set { if (value < 0 || value > 100) throw new ArgumentOutOfRangeException(); _value = value; } }
        public Grade(int v) { Value = v; }
    }
    public class StudentRef
    {
        public string Name { get; private set; }
        public Grade Grade { get; private set; }
        public StudentRef(string name) { Name = name; }
        public void AssignGrade(Grade g)
        {
            if (g == null) throw new ArgumentNullException("g");
            Grade = g; // проверка корректности в Grade.Value
        }
    }

    // 4. struct Time (immutable-style)
    public struct TimeVal
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public TimeVal(int h, int m)
        {
            if (h < 0) h = 0; if (m < 0) m = 0;
            Hours = h + (m / 60);
            Minutes = m % 60;
        }
        public TimeVal AddMinutes(int add)
        {
            int total = Hours * 60 + Minutes + add;
            if (total < 0) total = 0;
            return new TimeVal(total / 60, total % 60);
        }
    }

    // 5. Book (reference types)
    public class BookRef
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public BookRef(string t, string a) { Title = t; Author = a; }
        public string Info() { return $"\"{Title}\" — {Author}"; }
    }

    // 6. struct Rectangle (value)
    public struct RectVal
    {
        public double Width, Height;
        public RectVal(double w, double h) { Width = w; Height = h; }
        public double Area() { return Width * Height; }
    }
}