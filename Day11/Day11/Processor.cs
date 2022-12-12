using System.Runtime.CompilerServices;
using System.Text;

namespace Day11;

public class Processor
{
    private readonly string _fileName = @"D:\Projects\AdventOfCode2022\Day11\Day11\data.txt";

    List<Monkey> _monkeys = new();

    public void Run()
    {
        LoadMonkeys();
        CommonMultiple();

        for (int round = 0; round < 10000; round++)
        {
            foreach (var monkey in _monkeys)
            {
                monkey.Round();
            }

            if ((round + 1) % 1000 == 0)
            {
                Console.WriteLine($"\nAfter round {round + 1}, the monkeys are holding items with these worry levels:");
                foreach (var monkey in _monkeys)
                {
                    monkey.DumpItems();
                }
            }
        }

        Console.WriteLine($"\nNumber of items inspected by each monkey:");
        foreach (var monkey in _monkeys)
        {
            Console.WriteLine($"Monkey {monkey.Id} inspected {monkey.NumberOfInspections} items");
        }

        long monkeyBusinessLevel = MonkeyBusiness();
        Console.WriteLine($"\nLevel of monkey business: {monkeyBusinessLevel}");
    }

    private void CommonMultiple()
    {
        int cm = 1;
        foreach (var monkey in _monkeys)
        {
            cm *= monkey.Divisor ?? 1;
        }

        foreach (var monkey in _monkeys)
        {
            monkey.CommonMultiple = cm;
        }
    }

    private long MonkeyBusiness()
    {
        long max1 = long.MinValue;
        long max2 = long.MaxValue;
        foreach (var monkey in _monkeys)
        {
            if (monkey.NumberOfInspections > max1)
            {
                max2 = max1;
                max1 = monkey.NumberOfInspections;
            }
            else if (monkey.NumberOfInspections > max2)
            {
                max2 = monkey.NumberOfInspections;
            }
        }

        return max1 * max2;
    }

    private void LoadMonkeys()
    {
        Monkey monkey = new(_monkeys);
        foreach (var info in File.ReadLines(_fileName))
        {
            if (info.Trim().Length == 0)
            {
                _monkeys.Add(monkey);
                monkey = new(_monkeys);
                continue;
            }

            monkey.AddInfo(info);
        }

        _monkeys.Add(monkey);
    }
}
