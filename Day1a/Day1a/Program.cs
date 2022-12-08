List<Elf> elves = new();

int elfId = 0;
Elf elf = new();

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day1a\Day1a\data.txt"))
{
    if (int.TryParse(line, out int calories))
    {
        elf.Id = elfId;
        elf.Calories += calories;
        continue;
    }

    ++elfId;
    elves.Add(elf);
    elf = new Elf();
}

elves.Add(elf);

var sortedElves = elves.OrderByDescending(e => e.Calories).Take(3).ToList();
var total = sortedElves.Sum(e => e.Calories);

Console.WriteLine($"Total calories carried by top three elves: {total}");

public class Elf
{
    public int Id { get; set; } = 0;
    public int Calories { get; set; } = 0;
}
