using System.Reflection.Metadata.Ecma335;

namespace Day11;

public class Monkey
{
    List<Monkey>? _monkeys;

    private readonly long _worryValue = 1;
    
    private int _numberOfInspections = 0;
    public int NumberOfInspections => _numberOfInspections;

    public long CommonMultiple { get; set; } = 0;
    public int Id { get; set; } = -1;
    public Queue<long> Items { get; set; } = new();
    public string Value1 { get; set; } = "";
    public string Value2 { get; set; } = "";
    public string Operator { get; set; } = "";
    public int? Divisor { get; set; }
    public int? TrueMonkey { get; set; }
    public int? FalseMonkey { get; set; }

    public Monkey(List<Monkey>? monkeys)
    {
        _monkeys = monkeys;
    }

    public void AddInfo(string info)
    {
        var values = info.Trim().Split(' ');

        switch (values)
        {
            case ["Monkey", var id]:
                {
                    id = id.Substring(0, id.Length - 1);
                    Id = int.Parse(id);
                    return;
                }
            case ["Test:", .., var devisor]:
                {
                    Divisor = int.Parse(devisor);
                    return;
                }
            case ["If", "true:", .., var monkeyId]:
                {
                    TrueMonkey = int.Parse(monkeyId);
                    return;
                }
            case ["If", "false:", .., var monkeyId]:
                {
                    FalseMonkey = int.Parse(monkeyId);
                    return;
                }
            case ["Operation:", .., var value1, var operation, var value2]:
                {
                    Value1 = value1;
                    Value2 = value2;
                    Operator = operation;
                    return;
                }
            case ["Starting", ..]:
                {
                    foreach (var value in values)
                    {
                        string itemValue = value;
                        if (value.EndsWith(',')) itemValue = value.Substring(0, value.Length - 1);
                        if (int.TryParse(itemValue, out int item))
                        {
                            Items.Enqueue(item);
                        }
                    }
                    return;
                }
            default:
                throw new NotImplementedException();
        }
    }

    public void Round()
    {
        while (Items.Any())
        {
            _numberOfInspections++;
            long item = Items.Dequeue();
            long worryLevel = GetWorryLevel(item);

            int? monkeyId = worryLevel % Divisor == 0 ? TrueMonkey : FalseMonkey;
            ThrowItemToMonkey(worryLevel % CommonMultiple, monkeyId);
        }
    }

    private void ThrowItemToMonkey(long worryLevel, int? monkeyId)
    {
        if (_monkeys == null) throw new ArgumentNullException(nameof(_monkeys));
        var monkey = _monkeys.First(m => m.Id == monkeyId);
        monkey.Items.Enqueue(worryLevel);
    }

    private long GetWorryLevel(long oldValue)
    {
        long value1 = GetValue(Value1, oldValue);
        long value2 = GetValue(Value2, oldValue);

        switch (Operator)
        {
            case "+":
                return checked (value1 + value2) / _worryValue;
            case "*":
                return checked (value1 * value2) / _worryValue;
            default:
                throw new NotImplementedException();
        }
    }

    private long GetValue(string value, long oldValue)
    {
        if (value == "old") return oldValue;
        if (long.TryParse(value, out long result)) return result;
        throw new ArgumentOutOfRangeException();
    }

    internal void DumpItems()
    {
        bool firstPass = true;
        Console.Write($"Monkey {Id}: ");
        foreach (var item in Items)
        {
            if (firstPass)
            {
                Console.Write($"{item}");
                firstPass = false;
            }
            else
            {
                Console.Write($", {item}");
            }
        }
        Console.WriteLine();
    }
}
