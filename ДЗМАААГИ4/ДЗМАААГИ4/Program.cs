using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks4_1_to_4_15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 4.1 Vector2D: + and -
            var v1 = new Vector2D(2, 3);
            var v2 = new Vector2D(-1, 5);
            var vSum = v1 + v2;
            var vDiff = v1 - v2;
            Console.WriteLine($"4.1  v1+v2 = {vSum},  v1-v2 = {vDiff}");

            // 4.2 Matrix: * and / by number (and print)
            var m = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } }); // 3x2
            var mMul = m * 5;
            var mDiv = m / 5.0;
            Console.WriteLine("4.2  M * 5 ="); mMul.Print();
            Console.WriteLine("     M / 5 ="); mDiv.Print();

            // 4.3 Numbers: property Num and operator > (compare doubles typed by user - демонстрация с готовыми значениями)
            var nA = NumbersForCompare.FromDouble(3.14);
            var nB = NumbersForCompare.FromDouble(2.718);
            Console.WriteLine($"4.3  {nA.Value} > {nB.Value} ?  {(nA > nB)}");
            // Пример чтения с клавиатуры (раскомментируйте для реального ввода):
            // var n1 = NumbersForCompare.FromConsole();
            // var n2 = NumbersForCompare.FromConsole();
            // Console.WriteLine(n1 > n2);

            // 4.4 Numbers == and !=
            var eq1 = new NumbersEq(10);
            var eq2 = new NumbersEq(10);
            var eq3 = new NumbersEq(7);
            Console.WriteLine($"4.4  10 == 10 ? {(eq1 == eq2)} ; 10 != 7 ? {(eq1 != eq3)}");

            // 4.5 Calculator + and -, Print
            var c1 = new Calculator(7);
            var c2 = new Calculator(5);
            int sum = c1 + c2;
            int diff = c1 - c2;
            Calculator.Print(sum, "sum");
            Calculator.Print(diff, "diff");

            // 4.6 Array (массив) * and / by int
            var arr = new IntArray(new[] { 1, 2, 3, 4 });      // Задание "Array": используем имя IntArray, чтобы не конфликтовать с System.Array
            var arrMul = arr * 3;
            var arrDiv = arr / 2;
            Console.WriteLine($"4.6  arr*3  = {arrMul}");
            Console.WriteLine($"     arr/2  = {arrDiv}");

            // 4.7 Matrix: + and * by int, Print
            var mA = new Matrix(new double[,] { { 1, 1 }, { 2, 2 } });
            var mB = new Matrix(new double[,] { { 3, 3 }, { 4, 4 } });
            Console.WriteLine("4.7  mA initial:"); mA.Print();
            Console.WriteLine("     mA + mB:"); (mA + mB).Print();
            Console.WriteLine("     mA * 10:"); (mA * 10).Print();

            // 4.8 Polynomial +
            var p1 = new Polynomial(new double[] { 1, 2, 3 });       // 1 + 2x + 3x^2
            var p2 = new Polynomial(new double[] { 4, 0, -1, 5 });   // 4 - x^2 + 5x^3
            var pSum = p1 + p2;
            Console.WriteLine($"4.8  (1+2x+3x^2) + (4 - x^2 + 5x^3) = {pSum}");

            // 4.9 Temperature >
            var t1 = new Temperature(36.6);
            var t2 = new Temperature(22.0);
            Console.WriteLine($"4.9  {t1} > {t2} ? {(t1 > t2)}");

            // 4.10 Point - (distance)
            var pt1 = new Point(0, 0);
            var pt2 = new Point(3, 4);
            double dist = pt2 - pt1;
            Console.WriteLine($"4.10 distance(pt2, pt1) = {dist}");

            // 4.11 Triangle == (area eq)
            var tr1 = new Triangle(3, 4, 5);
            var tr2 = new Triangle(6, 8, 10); // площадь та же (масштаб 2x => площадь 4x, но у 3-4-5 площадь 6, у 6-8-10 площадь 24; для примера создадим equal)
            var tr3 = new Triangle(3, 4, 5);
            Console.WriteLine($"4.11 tr1 == tr2 ? {(tr1 == tr2)} ; tr1 == tr3 ? {(tr1 == tr3)}");

            // 4.12 Car ++ (speed +10)
            var car = new Car(50);
            car++;
            Console.WriteLine($"4.12 speed after ++ : {car.Speed}");

            // 4.13 Currency *
            var cur = new Currency(100m, "USD");
            var converted = cur * 1.1m; // умножаем сумму на курс
            Console.WriteLine($"4.13 {cur} * 1.1 = {converted}");

            // 4.14 Song != (by duration)
            var s1 = new Song("A", TimeSpan.FromSeconds(210));
            var s2 = new Song("B", TimeSpan.FromSeconds(210));
            var s3 = new Song("C", TimeSpan.FromSeconds(180));
            Console.WriteLine($"4.14 A != B ? {(s1 != s2)} ; A != C ? {(s1 != s3)}");

            // 4.15 Employee > (by salary)
            var e1 = new Employee("Alice", 120000);
            var e2 = new Employee("Bob", 95000);
            Console.WriteLine($"4.15 Alice > Bob ? {(e1 > e2)}");
        }
    }

    // ===== 4.1 =====
    public readonly struct Vector2D
    {
        public double X { get; }
        public double Y { get; }
        public Vector2D(double x, double y) { X = x; Y = y; }
        public static Vector2D operator +(Vector2D a, Vector2D b) => new Vector2D(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new Vector2D(a.X - b.X, a.Y - b.Y);
        public override string ToString() => $"({X}; {Y})";
    }

    // ===== 4.2, 4.7 =====
    public class Matrix
    {
        private readonly double[,] data;
        public int Rows => data.GetLength(0);
        public int Cols => data.GetLength(1);

        public Matrix(double[,] initial) => data = (double[,])initial.Clone();

        public double this[int r, int c]
        {
            get => data[r, c];
            set => data[r, c] = value;
        }

        // element-wise addition
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Cols != b.Cols) throw new ArgumentException("Matrix sizes differ");
            var res = new double[a.Rows, a.Cols];
            for (int i = 0; i < a.Rows; i++)
                for (int j = 0; j < a.Cols; j++)
                    res[i, j] = a.data[i, j] + b.data[i, j];
            return new Matrix(res);
        }

        // scalar multiply (int)
        public static Matrix operator *(Matrix m, int k)
        {
            var res = new double[m.Rows, m.Cols];
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Cols; j++)
                    res[i, j] = m.data[i, j] * k;
            return new Matrix(res);
        }

        // scalar multiply/divide (double) — для 4.2
        public static Matrix operator *(Matrix m, double k)
        {
            var res = new double[m.Rows, m.Cols];
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Cols; j++)
                    res[i, j] = m.data[i, j] * k;
            return new Matrix(res);
        }
        public static Matrix operator /(Matrix m, double k)
        {
            if (Math.Abs(k) < 1e-12) throw new DivideByZeroException();
            var res = new double[m.Rows, m.Cols];
            for (int i = 0; i < m.Rows; i++)
                for (int j = 0; j < m.Cols; j++)
                    res[i, j] = m.data[i, j] / k;
            return new Matrix(res);
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write($"{data[i, j],8:0.###}");
                Console.WriteLine();
            }
        }
    }

    // ===== 4.3 =====
    public class NumbersForCompare
    {
        public double Value { get; private set; }
        public NumbersForCompare(double value) => Value = value;

        public static NumbersForCompare FromDouble(double d) => new NumbersForCompare(d);

        public static NumbersForCompare FromConsole()
        {
            Console.Write("Enter a real number: ");
            if (double.TryParse(Console.ReadLine(), out var d)) return new NumbersForCompare(d);
            return new NumbersForCompare(0);
        }

        public static bool operator >(NumbersForCompare a, NumbersForCompare b) => a.Value > b.Value;
        public static bool operator <(NumbersForCompare a, NumbersForCompare b) => a.Value < b.Value; // парный оператор
    }

    // ===== 4.4 =====
    public class NumbersEq
    {
        public int Num { get; }
        public NumbersEq(int n) => Num = n;

        public static bool operator ==(NumbersEq a, NumbersEq b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Num == b.Num;
        }
        public static bool operator !=(NumbersEq a, NumbersEq b) => !(a == b);

        public override bool Equals(object obj) => obj is NumbersEq other && other.Num == Num;
        public override int GetHashCode() => Num.GetHashCode();
    }

    // ===== 4.5 =====
    public class Calculator
    {
        public int Value { get; }
        public Calculator(int v) => Value = v;

        public static int operator +(Calculator a, Calculator b) => a.Value + b.Value;
        public static int operator -(Calculator a, Calculator b) => a.Value - b.Value;

        public static void Print(int result, string label = null)
            => Console.WriteLine($"4.5  {(label ?? "result")}: {result}");
    }

    // ===== 4.6 =====  (класс для задания "Array" — назван IntArray, чтобы не конфликтовать с System.Array)
    public class IntArray
    {
        private readonly int[] data;
        public IntArray(int[] initial) => data = (int[])initial.Clone();

        public static IntArray operator *(IntArray arr, int k)
        {
            var res = arr.data.Select(x => x * k).ToArray();
            return new IntArray(res);
        }
        public static IntArray operator /(IntArray arr, int k)
        {
            if (k == 0) throw new DivideByZeroException();
            var res = arr.data.Select(x => x / k).ToArray();
            return new IntArray(res);
        }

        public override string ToString() => "[" + string.Join(", ", data) + "]";
    }

    // ===== 4.8 =====
    public class Polynomial
    {
        // coeffs[i] — коэффициент при x^i
        private readonly List<double> coeffs;
        public Polynomial(IEnumerable<double> coeffs) => this.coeffs = Normalize(coeffs.ToList());

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            int n = Math.Max(a.coeffs.Count, b.coeffs.Count);
            var res = new double[n];
            for (int i = 0; i < n; i++)
            {
                double ai = i < a.coeffs.Count ? a.coeffs[i] : 0;
                double bi = i < b.coeffs.Count ? b.coeffs[i] : 0;
                res[i] = ai + bi;
            }
            return new Polynomial(res);
        }

        private static List<double> Normalize(List<double> cs)
        {
            for (int i = cs.Count - 1; i > 0 && Math.Abs(cs[i]) < 1e-12; i--) cs.RemoveAt(i);
            return cs;
        }

        public override string ToString()
        {
            if (coeffs.All(c => Math.Abs(c) < 1e-12)) return "0";
            var terms = new List<string>();
            for (int i = coeffs.Count - 1; i >= 0; i--)
            {
                double c = coeffs[i];
                if (Math.Abs(c) < 1e-12) continue;

                string term;
                if (i == 0) term = string.Format("{0:0.###}", c);
                else if (i == 1) term = string.Format("{0:0.###}x", c);
                else term = string.Format("{0:0.###}x^{1}", c, i);

                terms.Add(term);
            }
            return string.Join(" + ", terms);
        }
    }

    // ===== 4.9 =====
    public readonly struct Temperature
    {
        public double Celsius { get; }
        public Temperature(double c) { Celsius = c; }
        public static bool operator >(Temperature a, Temperature b) => a.Celsius > b.Celsius;
        public static bool operator <(Temperature a, Temperature b) => a.Celsius < b.Celsius;
        public override string ToString() => $"{Celsius:0.##}°C";
    }

    // ===== 4.10 =====
    public readonly struct Point
    {
        public double X { get; }
        public double Y { get; }
        public Point(double x, double y) { X = x; Y = y; }
        // расстояние между точками
        public static double operator -(Point a, Point b)
        {
            double dx = a.X - b.X, dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    // ===== 4.11 =====
    public class Triangle
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0 || a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException("Invalid triangle sides");
            A = a; B = b; C = c;
        }
        public double Area
        {
            get
            {
                double s = (A + B + C) / 2.0;
                return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
            }
        }
        public static bool operator ==(Triangle t1, Triangle t2)
        {
            if (ReferenceEquals(t1, t2)) return true;
            if (t1 is null || t2 is null) return false;
            return Math.Abs(t1.Area - t2.Area) < 1e-9;
        }
        public static bool operator !=(Triangle t1, Triangle t2) => !(t1 == t2);
        public override bool Equals(object obj) => obj is Triangle t && this == t;
        public override int GetHashCode() => Area.GetHashCode();
    }

    // ===== 4.12 =====
    public class Car
    {
        public int Speed { get; private set; }
        public Car(int speed = 0) => Speed = speed;

        public static Car operator ++(Car c)
        {
            c.Speed += 10;
            return c;
        }
    }

    // ===== 4.13 =====
    public class Currency
    {
        public decimal Amount { get; }
        public string Code { get; }
        public Currency(decimal amount, string code) { Amount = amount; Code = code; }

        public static Currency operator *(Currency cur, decimal rate)
            => new Currency(cur.Amount * rate, cur.Code);

        public override string ToString() => $"{Amount:0.##} {Code}";
    }

    // ===== 4.14 =====
    public class Song
    {
        public string Title { get; }
        public TimeSpan Duration { get; }
        public Song(string title, TimeSpan duration) { Title = title; Duration = duration; }

        public static bool operator !=(Song a, Song b)
        {
            if (ReferenceEquals(a, b)) return false;
            if (a is null || b is null) return true;
            return a.Duration != b.Duration;
        }
        public static bool operator ==(Song a, Song b) => !(a != b);
        public override bool Equals(object obj) => obj is Song s && s.Duration == Duration;
        public override int GetHashCode() => Duration.GetHashCode();
    }

    // ===== 4.15 =====
    public class Employee
    {
        public string Name { get; }
        public decimal Salary { get; }
        public Employee(string name, decimal salary) { Name = name; Salary = salary; }
        public static bool operator >(Employee a, Employee b) => a.Salary > b.Salary;
        public static bool operator <(Employee a, Employee b) => a.Salary < b.Salary;
    }
}
