using System;

namespace Tasks1_1_to_1_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1.1
            Car car1 = new Car();

            // 1.2
            Vegetable veg = new Vegetable { Name = "Tomato", Color = "Red" };

            // 1.3
            Food food1 = new Food { Name = "Apple", Weight = 150 };
            food1.ShowInfo();

            // 1.4
            Account acc = new Account();
            acc.Deposit(1000m);

            // 1.5
            Student st = new Student("Alice");
            st.ShowName();

            // 1.6
            FoodPublic food2 = new FoodPublic { Name = "Banana", Weight = 120 };
            Console.WriteLine($"Public fields: {food2.Name}, {food2.Weight}");
            food2.ShowInfo();

            // 1.7
            Movie movie1 = new Movie();

            // 1.8
            Staff emp = new Staff { FullName = "John Smith" };
            emp.SetSalary(120000);

            // 1.9
            Book b = new Book("Clean Code", "Robert C. Martin");
            b.Print();

            // 1.10
            Multiplication mul = new Multiplication { Number = 7 };
            Console.WriteLine($"Product: {mul.MultiplicationNum(6)}");

            // 1.11
            Compare cmp = new Compare();
            cmp.CompareTo();

            // 1.12
            StudentInfo stud = new StudentInfo("Paul", 22);
            stud.ShowInfo();

            // 1.13
            CarInfo auto = new CarInfo("Toyota", 2022);

            // 1.14
            Restaurant rest = new Restaurant("Mount & Grill");
            rest.ShowRestaurant();

            // 1.15
            MusicTool tool = new MusicTool { Type = "Guitar" };
            tool.Play();

            // 1.16
            User user = new User { Login = "demo_user", Password = "P@ssw0rd" };
            user.ShowInfo();

            // 1.17
            Film film = new Film("Matrix", "Science Fiction");

            Console.WriteLine("\nAll tasks 1.1–1.17 done.");
        }
    }

    public class Car { }

    public class Vegetable
    {
        public string Name;
        public string Color;
    }

    public class Food
    {
        public string Name;
        public int Weight;
        public void ShowInfo() => Console.WriteLine($"Food: {Name}, {Weight} g");
    }

    public class Account
    {
        private decimal _balance;
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                Console.WriteLine($"Balance: {_balance}");
            }
        }
    }

    public class Student
    {
        private string name;
        public Student(string name) => this.name = name;
        public void ShowName() => Console.WriteLine($"Student name: {name}");
    }

    public class FoodPublic
    {
        public string Name;
        public int Weight;
        public void ShowInfo() => Console.WriteLine($"[Public] Food: {Name}, {Weight} g");
    }

    public class Movie { }

    public class Staff
    {
        public string FullName;
        private int Salary;
        public void SetSalary(int salary) => Salary = salary;
    }

    public class Book
    {
        public string Title;
        public string Author;
        public Book(string title, string author) { Title = title; Author = author; }
        public void Print() => Console.WriteLine($"Book: \"{Title}\", Author: {Author}");
    }

    public class Multiplication
    {
        public int Number;
        public int MultiplicationNum(int other) => Number * other;
    }

    public class Compare
    {
        public void CompareTo()
        {
            int x = 42, y = 58;
            Console.WriteLine($"Max: {(x > y ? x : y)}");
        }
    }

    public class StudentInfo
    {
        private string Name;
        private int Age;
        public StudentInfo(string name, int age) { Name = name; Age = age; }
        public void ShowInfo() => Console.WriteLine($"Student: {Name}, Age: {Age}");
    }

    public class CarInfo
    {
        private string Brand;
        private int Year;
        public CarInfo(string brand, int year) { Brand = brand; Year = year; }
    }

    public class Restaurant
    {
        private string Name;
        public Restaurant(string name) => Name = name;
        public void ShowRestaurant() => Console.WriteLine($"Welcome to restaurant \"{Name}\"!");
    }

    public class MusicTool
    {
        public string Type;
        public void Play() => Console.WriteLine($"{Type} is playing!");
    }

    public class User
    {
        public string Login;
        public string Password;
        public void ShowInfo() => Console.WriteLine($"Login: {Login}");
    }

    public class Film
    {
        private string Title;
        private string Genre;
        public Film(string title, string genre) { Title = title; Genre = genre; }
    }
}
