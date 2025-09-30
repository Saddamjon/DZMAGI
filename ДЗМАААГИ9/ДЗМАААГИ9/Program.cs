using System;
using System.Collections.Generic;

namespace Tasks9_All
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ===== 9.1 Student + List<grades> методы =====
            var st = new Student { Name = "Alex", Grades = new List<int>() };
            st.Grades.Add(5);
            st.Grades.Add(4);
            st.Grades.Add(3);
            Console.WriteLine("9.1 Начальные оценки: " + string.Join(", ", st.Grades));

            st.Grades.RemoveAt(1); // удалить элемент с индексом 1
            Console.WriteLine("    После RemoveAt(1): " + string.Join(", ", st.Grades));

            Console.WriteLine("    Contains(5)? " + st.Grades.Contains(5));

            st.Grades.Reverse();
            Console.WriteLine("    После Reverse(): " + string.Join(", ", st.Grades));

            st.Grades.Clear();
            Console.WriteLine("    После Clear(): Count=" + st.Grades.Count);

            Console.WriteLine();

            // ===== 9.2 Проверка скобок стеком =====
            string expr = "{[5x + 3)+4]*5-(8+3)}*[4x+2]";
            bool ok = BracketsBalanced(expr, out string msg);
            Console.WriteLine("9.2 Выражение: " + expr);
            Console.WriteLine("    Корректность скобок: " + ok + (msg != null ? " (" + msg + ")" : ""));
            Console.WriteLine();

            // ===== 9.3 Queue с разными типами =====
            Queue<object> q = new Queue<object>();
            q.Enqueue(42);
            q.Enqueue("hello");
            q.Enqueue(3.14);
            q.Enqueue(true);

            Console.WriteLine("9.3 Извлечение из очереди:");
            while (q.Count > 0)
            {
                object item = q.Dequeue();
                Console.WriteLine("    -> " + item + "  (тип: " + item.GetType().Name + ")");
            }
            Console.WriteLine();

            // ===== 9.4 Dictionary магазин =====
            Dictionary<string, double> shop = new Dictionary<string, double>
            {
                ["Хлеб"] = 1.20,
                ["Молоко"] = 0.95,
                ["Яблоки"] = 2.50
            };
            string key = "Молоко";
            Console.WriteLine("9.4 Цена товара \"" + key + "\": " + shop[key]);
            Console.WriteLine();

            // ===== 9.5 Book + словарь книг =====
            var books = new Dictionary<string, Book>
            {
                ["Clean Code"] = new Book { Name = "Clean Code", Author = "Robert C. Martin", Year = 2008 },
                ["CLR via C#"] = new Book { Name = "CLR via C#", Author = "Jeffrey Richter", Year = 2012 },
                ["The Pragmatic Programmer"] = new Book { Name = "The Pragmatic Programmer", Author = "Andrew Hunt, David Thomas", Year = 1999 }
            };

            Console.WriteLine("9.5 Список книг:");
            foreach (var kv in books)
            {
                var b = kv.Value;
                Console.WriteLine($"    - {b.Name} — {b.Author} ({b.Year})");
            }
        }

        // === 9.2 ===
        static bool BracketsBalanced(string s, out string message)
        {
            var stack = new Stack<char>();
            var pairs = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (ch == '(' || ch == '[' || ch == '{')
                {
                    stack.Push(ch);
                }
                else if (ch == ')' || ch == ']' || ch == '}')
                {
                    if (stack.Count == 0)
                    {
                        message = $"Лишняя закрывающая скобка '{ch}' на позиции {i}.";
                        return false;
                    }
                    char open = stack.Pop();
                    if (open != pairs[ch])
                    {
                        message = $"Несоответствие: ожидалась закрывающая для '{open}', встретили '{ch}' на позиции {i}.";
                        return false;
                    }
                }
            }
            if (stack.Count > 0)
            {
                message = $"Остались незакрытые скобки: {string.Join("", stack)}.";
                return false;
            }
            message = null;
            return true;
        }
    }

    // === 9.1 ===
    public class Student
    {
        public string Name { get; set; }
        public List<int> Grades { get; set; }
    }

    // === 9.5 ===
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
