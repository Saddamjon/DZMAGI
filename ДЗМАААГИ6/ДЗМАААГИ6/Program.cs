using System;

namespace Tasks6_All
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ===== 6.1 BankAccount =====
            var acc = new BankAccount(500m);
            acc.Put(250m);
            acc.Withdraw(100m);
            Console.WriteLine($"6.1 Баланс: {acc.Balance:C}");

            // (необязательно) демонстрация переопределения Put в наследнике
            var bonus = new BonusAccount(500m);
            bonus.Put(100m); // начислит +1% бонуса
            Console.WriteLine($"6.1 BonusAccount баланс: {bonus.Balance:C}");

            // ===== 6.2 Figure (virtual Square) -> Rectangle/Circle =====
            Figure f1 = new Rectangle(4.0, 2.5);
            Figure f2 = new Circle(3.0);
            Console.WriteLine($"6.2 Площадь прямоугольника: {f1.Square():0.###}");
            Console.WriteLine($"    Площадь круга:          {f2.Square():0.###}");

            // ===== 6.3 Animal (abstract) -> Cat/Dog =====
            Animal cat = new Cat();
            Animal dog = new Dog();
            Console.Write("6.3 Кошка: "); cat.MakeSound();
            Console.Write("    Собака: "); dog.MakeSound();

            // ===== 6.4 Zoo (abstract) -> Elephant/Lion/Giraffe =====
            Zoo elephantZone = new Elephant(12); // 12 слонов
            Zoo lionZone = new Lion(8);
            Zoo giraffeZone = new Giraffe(5);
            Console.WriteLine($"6.4 Площадь (слоны):   {elephantZone.CalculateZooArea():0.###}");
            Console.WriteLine($"    Площадь (львы):    {lionZone.CalculateZooArea():0.###}");
            Console.WriteLine($"    Площадь (жирафы):  {giraffeZone.CalculateZooArea():0.###}");

            // ===== 6.5 IElectronicDevice -> Television =====
            IElectronicDevice tv = new Television();
            tv.TurnOn();
            tv.SetVolume(35);
            tv.TurnOff();
        }
    }

    // ===== 6.1 =====
    public class BankAccount
    {
        public decimal Balance { get; protected set; }

        public BankAccount() : this(0m) { }
        public BankAccount(decimal initial) { if (initial >= 0) Balance = initial; }

        public virtual void Put(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сумма пополнения должна быть положительной.");
                return;
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сумма снятия должна быть положительной.");
                return;
            }
            if (amount > Balance)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }
            Balance -= amount;
        }
    }

    // пример наследника, переопределяющего Put (необязательно по условию, но показывает виртуальность)
    public class BonusAccount : BankAccount
    {
        public BonusAccount(decimal initial) : base(initial) { }
        public override void Put(decimal amount)
        {
            base.Put(amount);
            if (amount > 0) Balance += amount * 0.01m; // +1% бонуса
        }
    }

    // ===== 6.2 =====
    public class Figure
    {
        public virtual double Square() { return 0.0; }
    }

    public class Rectangle : Figure
    {
        public double Width { get; }
        public double Height { get; }
        public Rectangle(double w, double h) { Width = w; Height = h; }
        public override double Square() { return Width * Height; }
    }

    public class Circle : Figure
    {
        public double Radius { get; }
        public Circle(double r) { Radius = r; }
        public override double Square() { return Math.PI * Radius * Radius; }
    }

    // ===== 6.3 =====
    public abstract class Animal
    {
        public abstract void MakeSound();
    }

    public class Cat : Animal
    {
        public override void MakeSound() { Console.WriteLine("Meow!"); }
    }

    public class Dog : Animal
    {
        public override void MakeSound() { Console.WriteLine("Woof!"); }
    }

    // ===== 6.4 =====
    public abstract class Zoo
    {
        protected int numberOfAnimals;
        protected Zoo(int numberOfAnimals)
        {
            if (numberOfAnimals < 0) numberOfAnimals = 0;
            this.numberOfAnimals = numberOfAnimals;
        }
        public abstract double CalculateZooArea();
    }

    public class Elephant : Zoo
    {
        public Elephant(int n) : base(n) { }
        public override double CalculateZooArea() { return numberOfAnimals * 10.0; }
    }

    public class Lion : Zoo
    {
        public Lion(int n) : base(n) { }
        public override double CalculateZooArea() { return numberOfAnimals * 5.0; }
    }

    public class Giraffe : Zoo
    {
        public Giraffe(int n) : base(n) { }
        public override double CalculateZooArea() { return numberOfAnimals * 7.0; }
    }

    // ===== 6.5 =====
    public interface IElectronicDevice
    {
        void TurnOn();
        void TurnOff();
        void SetVolume(int level);
    }

    public class Television : IElectronicDevice
    {
        private bool _isOn;
        private int _volume; // 0..100

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine("Телевизор включен.");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine("Телевизор выключен.");
        }

        public void SetVolume(int level)
        {
            if (!_isOn)
            {
                Console.WriteLine("Нельзя менять громкость: телевизор выключен.");
                return;
            }
            if (level < 0) level = 0;
            if (level > 100) level = 100;
            _volume = level;
            Console.WriteLine($"Громкость установлена: {_volume}");
        }
    }
}
