using System;
using System.IO;

namespace Tasks7_All
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ===== 7.1 =====
            Console.WriteLine("7.1 Чтение файла с finally:");
            string path = "test.txt";
            File.WriteAllText(path, "Пример содержимого файла."); // создадим тестовый файл
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path);
                string content = reader.ReadToEnd();
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении файла: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                Console.WriteLine("Файл закрыт.\n");
            }

            // ===== 7.2 =====
            Console.WriteLine("7.2 Деление с обработкой /0:");
            Console.WriteLine("10 / 2 = " + SafeDivide(10, 2));
            Console.WriteLine("10 / 0 = " + SafeDivide(10, 0) + "\n");

            // ===== 7.3 =====
            Console.WriteLine("7.3 Квадратный корень:");
            Console.Write("Введите число: ");
            try
            {
                double num = double.Parse(Console.ReadLine());
                if (num < 0) throw new ArgumentException("Нельзя извлечь корень из отрицательного числа.");
                Console.WriteLine("√" + num + " = " + Math.Sqrt(num));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            Console.WriteLine();

            // ===== 7.4 =====
            Console.WriteLine("7.4 Генерация исключения в конструкторе:");
            try
            {
                InvalidClass obj = new InvalidClass(-5); // недопустимый параметр
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение при создании объекта: " + ex.Message);
            }
            Console.WriteLine();

            // ===== 7.5 =====
            Console.WriteLine("7.5 Сумма двух чисел:");
            try
            {
                Console.Write("Введите первое целое число: ");
                int a = int.Parse(Console.ReadLine());
                Console.Write("Введите второе целое число: ");
                int b = int.Parse(Console.ReadLine());
                Console.WriteLine("Сумма = " + (a + b));
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: нужно ввести именно целые числа!");
            }
        }

        // для 7.2
        static double SafeDivide(int a, int b)
        {
            try
            {
                return a / b;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Деление на ноль!");
                return double.NaN;
            }
        }
    }

    // для 7.4
    public class InvalidClass
    {
        public int Value { get; private set; }
        public InvalidClass(int value)
        {
            if (value < 0)
                throw new ArgumentException("Значение не может быть отрицательным.");
            Value = value;
        }
    }
}
