using System;

namespace Tasks8_All
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ===== 8.1 Чётное/нечётное =====
            Console.Write("8.1 Введите целое число: ");
            int n;
            if (int.TryParse(Console.ReadLine(), out n))
                Console.WriteLine(n % 2 == 0 ? "Число чётное" : "Число нечётное");
            else
                Console.WriteLine("Некорректный ввод (нужно целое число).");

            Console.WriteLine();

            // ===== 8.2 Банк: события =====
            var bank = new Bank();
            bank.NewAccountOpened += (s, e) => Console.WriteLine($"8.2 Открыт счёт #{e.AccountId} для {e.ClientName}, баланс {e.Balance:C}");
            bank.AccountClosed += (s, e) => Console.WriteLine($"8.2 Закрыт счёт #{e.AccountId}");
            bank.MoneyTransferred += (s, e) => Console.WriteLine($"8.2 Перевод {e.Amount:C} со счёта #{e.FromId} на #{e.ToId}");

            int accA = bank.OpenAccount("Иван", 1000m);
            int accB = bank.OpenAccount("Мария", 500m);
            bank.Transfer(accA, accB, 200m);
            bank.CloseAccount(accB);

            Console.WriteLine();

            // ===== 8.3 Издатель/Подписчик =====
            var publisher = new Publisher(threshold: 10);
            var sub1 = new Subscriber("A");
            var sub2 = new Subscriber("B");
            publisher.ThresholdReached += sub1.OnThresholdReached;
            publisher.ThresholdReached += sub2.OnThresholdReached;

            publisher.PushValue(7);   // не сработает
            publisher.PushValue(12);  // сработает событие

            Console.WriteLine();

            // ===== 8.4 Калькулятор с событием результата =====
            var calc = new Calculator();
            calc.OperationResult += (s, e) =>
                Console.WriteLine($"8.4 Операция '{e.Operation}': {e.A} и {e.B} => результат = {e.Result}");
            calc.Add(3, 5);
            calc.Div(10, 2);
            calc.Div(10, 0); // деление на 0 — покажет сообщение и не вызовет событие

            Console.WriteLine();

            // ===== 8.5 Умный дом: делегаты и события =====
            var hub = new HomeAutomationHub();

            // Подписчики
            var logger = new Logger();
            hub.LightToggled += logger.OnLightToggled;
            hub.TemperatureChanged += logger.OnTemperatureChanged;
            hub.DoorStateChanged += logger.OnDoorStateChanged;
            hub.WindowStateChanged += logger.OnWindowStateChanged;

            var notifier = new Notifier();
            hub.DoorStateChanged += notifier.OnDoorStateChanged;
            hub.WindowStateChanged += notifier.OnWindowStateChanged;

            var saver = new EnergySaver();
            hub.LightToggled += saver.OnLightToggled;
            hub.TemperatureChanged += saver.OnTemperatureChanged;

            // Действия
            hub.ToggleLight(room: "Гостиная", isOn: true);
            hub.ChangeTemperature(zone: "Детская", newTemp: 23);
            hub.SetDoorState(door: "Входная", isOpen: true);
            hub.SetWindowState(window: "Кухня", isOpen: true);

            Console.WriteLine("\nГотово.");
        }
    }

    // ===================== 8.2 Банк =====================
    // Делегаты и EventArgs
    public class AccountEventArgs : EventArgs
    {
        public int AccountId { get; }
        public string ClientName { get; }
        public decimal Balance { get; }
        public AccountEventArgs(int id, string client, decimal balance) { AccountId = id; ClientName = client; Balance = balance; }
    }
    public class TransferEventArgs : EventArgs
    {
        public int FromId { get; }
        public int ToId { get; }
        public decimal Amount { get; }
        public TransferEventArgs(int from, int to, decimal amount) { FromId = from; ToId = to; Amount = amount; }
    }

    public class Bank
    {
        private int _nextId = 1;
        private readonly System.Collections.Generic.Dictionary<int, decimal> _balances =
            new System.Collections.Generic.Dictionary<int, decimal>();
        private readonly System.Collections.Generic.Dictionary<int, string> _clients =
            new System.Collections.Generic.Dictionary<int, string>();

        public event EventHandler<AccountEventArgs> NewAccountOpened;
        public event EventHandler<AccountEventArgs> AccountClosed;
        public event EventHandler<TransferEventArgs> MoneyTransferred;

        public int OpenAccount(string clientName, decimal initialBalance)
        {
            int id = _nextId++;
            _clients[id] = clientName;
            _balances[id] = initialBalance < 0 ? 0 : initialBalance;
            NewAccountOpened?.Invoke(this, new AccountEventArgs(id, clientName, _balances[id]));
            return id;
        }

        public void CloseAccount(int id)
        {
            if (_balances.ContainsKey(id))
            {
                var name = _clients[id];
                var bal = _balances[id];
                _balances.Remove(id);
                _clients.Remove(id);
                AccountClosed?.Invoke(this, new AccountEventArgs(id, name, bal));
            }
        }

        public void Transfer(int fromId, int toId, decimal amount)
        {
            if (amount <= 0) return;
            if (!_balances.ContainsKey(fromId) || !_balances.ContainsKey(toId)) return;
            if (_balances[fromId] < amount) return;

            _balances[fromId] -= amount;
            _balances[toId] += amount;
            MoneyTransferred?.Invoke(this, new TransferEventArgs(fromId, toId, amount));
        }
    }

    // ===================== 8.3 Издатель/Подписчик =====================
    public class ThresholdEventArgs : EventArgs
    {
        public int Value { get; }
        public int Threshold { get; }
        public ThresholdEventArgs(int value, int threshold) { Value = value; Threshold = threshold; }
    }

    public class Publisher
    {
        private readonly int _threshold;
        public Publisher(int threshold) { _threshold = threshold; }
        public event EventHandler<ThresholdEventArgs> ThresholdReached;

        public void PushValue(int value)
        {
            if (value >= _threshold)
                ThresholdReached?.Invoke(this, new ThresholdEventArgs(value, _threshold));
        }
    }

    public class Subscriber
    {
        private readonly string _name;
        public Subscriber(string name) { _name = name; }
        public void OnThresholdReached(object sender, ThresholdEventArgs e)
        {
            Console.WriteLine($"8.3 Подписчик {_name}: порог {e.Threshold} достигнут значением {e.Value}");
        }
    }

    // ===================== 8.4 Калькулятор с событием =====================
    public class CalcEventArgs : EventArgs
    {
        public string Operation { get; }
        public double A { get; }
        public double B { get; }
        public double Result { get; }
        public CalcEventArgs(string op, double a, double b, double res) { Operation = op; A = a; B = b; Result = res; }
    }
    public class Calculator
    {
        public event EventHandler<CalcEventArgs> OperationResult;

        public double Add(double a, double b)
        {
            double r = a + b;
            OperationResult?.Invoke(this, new CalcEventArgs("Add", a, b, r));
            return r;
        }
        public double Sub(double a, double b)
        {
            double r = a - b;
            OperationResult?.Invoke(this, new CalcEventArgs("Sub", a, b, r));
            return r;
        }
        public double Mul(double a, double b)
        {
            double r = a * b;
            OperationResult?.Invoke(this, new CalcEventArgs("Mul", a, b, r));
            return r;
        }
        public double Div(double a, double b)
        {
            if (Math.Abs(b) < 1e-12)
            {
                Console.WriteLine("8.4 Деление на ноль невозможно.");
                return double.NaN;
            }
            double r = a / b;
            OperationResult?.Invoke(this, new CalcEventArgs("Div", a, b, r));
            return r;
        }
    }

    // ===================== 8.5 Умный дом: делегаты и события =====================
    public class LightEventArgs : EventArgs
    {
        public string Room { get; }
        public bool IsOn { get; }
        public LightEventArgs(string room, bool isOn) { Room = room; IsOn = isOn; }
    }
    public class TemperatureEventArgs : EventArgs
    {
        public string Zone { get; }
        public int NewTemp { get; }
        public TemperatureEventArgs(string zone, int newTemp) { Zone = zone; NewTemp = newTemp; }
    }
    public class DoorEventArgs : EventArgs
    {
        public string Door { get; }
        public bool IsOpen { get; }
        public DoorEventArgs(string door, bool isOpen) { Door = door; IsOpen = isOpen; }
    }
    public class WindowEventArgs : EventArgs
    {
        public string Window { get; }
        public bool IsOpen { get; }
        public WindowEventArgs(string window, bool isOpen) { Window = window; IsOpen = isOpen; }
    }

    public class HomeAutomationHub
    {
        public event EventHandler<LightEventArgs> LightToggled;
        public event EventHandler<TemperatureEventArgs> TemperatureChanged;
        public event EventHandler<DoorEventArgs> DoorStateChanged;
        public event EventHandler<WindowEventArgs> WindowStateChanged;

        public void ToggleLight(string room, bool isOn) =>
            LightToggled?.Invoke(this, new LightEventArgs(room, isOn));

        public void ChangeTemperature(string zone, int newTemp) =>
            TemperatureChanged?.Invoke(this, new TemperatureEventArgs(zone, newTemp));

        public void SetDoorState(string door, bool isOpen) =>
            DoorStateChanged?.Invoke(this, new DoorEventArgs(door, isOpen));

        public void SetWindowState(string window, bool isOpen) =>
            WindowStateChanged?.Invoke(this, new WindowEventArgs(window, isOpen));
    }

    // Подписчики
    public class Logger
    {
        public void OnLightToggled(object s, LightEventArgs e)
            => Console.WriteLine($"8.5 [ЛОГ] Свет в '{e.Room}': {(e.IsOn ? "вкл" : "выкл")}");
        public void OnTemperatureChanged(object s, TemperatureEventArgs e)
            => Console.WriteLine($"8.5 [ЛОГ] Температура в '{e.Zone}': {e.NewTemp}°C");
        public void OnDoorStateChanged(object s, DoorEventArgs e)
            => Console.WriteLine($"8.5 [ЛОГ] Дверь '{e.Door}': {(e.IsOpen ? "открыта" : "закрыта")}");
        public void OnWindowStateChanged(object s, WindowEventArgs e)
            => Console.WriteLine($"8.5 [ЛОГ] Окно '{e.Window}': {(e.IsOpen ? "открыто" : "закрыто")}");
    }

    public class Notifier
    {
        public void OnDoorStateChanged(object s, DoorEventArgs e)
        {
            if (e.IsOpen) Console.WriteLine($"8.5 [НОТИФИКАТОР] Внимание: дверь '{e.Door}' открыта!");
        }
        public void OnWindowStateChanged(object s, WindowEventArgs e)
        {
            if (e.IsOpen) Console.WriteLine($"8.5 [НОТИФИКАТОР] Внимание: окно '{e.Window}' открыто!");
        }
    }

    public class EnergySaver
    {
        public void OnLightToggled(object s, LightEventArgs e)
        {
            if (!e.IsOn) Console.WriteLine("8.5 [ЭКО] Свет выключен — экономим энергию.");
        }
        public void OnTemperatureChanged(object s, TemperatureEventArgs e)
        {
            if (e.NewTemp > 24) Console.WriteLine("8.5 [ЭКО] Температура высока — рекомендую снизить для экономии.");
        }
    }
}
