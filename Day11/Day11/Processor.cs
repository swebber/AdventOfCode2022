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

        for (int round = 0; round < 20; round++)
        {
            foreach (var monkey in _monkeys)
            {
                monkey.Round();
            }

            Console.WriteLine($"\nAfter round {round + 1}, the monkeys are holding items with these worry levels:");
            foreach (var monkey in _monkeys)
            {
                monkey.DumpItems();
            }
        }

        Console.WriteLine($"\nNumber of items inspected by each monkey:");
        foreach (var monkey in _monkeys)
        {
            Console.WriteLine($"Monkey {monkey.Id} inspected {monkey.NumberOfInspections} items");
        }

        int monkeyBusinessLevel = MonkeyBusiness();
        Console.WriteLine($"\nLevel of monkey business: {monkeyBusinessLevel}");
    }

    private int MonkeyBusiness()
    {
        int max1 = int.MinValue;
        int max2 = int.MaxValue;
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
