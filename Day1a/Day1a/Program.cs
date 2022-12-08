int elf = 1;
int maxElf = 0;
int elfCalories = 0;
int maxCalories = 0;

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day1a\Day1a\data.txt"))
{
    if (int.TryParse(line, out int calories))
    {
        elfCalories += calories;
        if (elfCalories > maxCalories)
        {
            maxElf = elf;
            maxCalories = elfCalories;
        }

        continue;
    }

    ++elf;
    elfCalories = 0;
}

Console.WriteLine($"Max Elf: {maxElf}\nMax Calories: {maxCalories}");
