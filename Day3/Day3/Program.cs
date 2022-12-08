string pointsIndex = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
int totalPriority = 0;

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day3\Day3\data.txt"))
{
    int lineLength = line.Length;
    string compartment1 = line.Substring(0, lineLength / 2);
    string compartment2 = line.Substring(lineLength / 2);

    var sharedType = compartment1.Intersect(compartment2).First();
    int priority = pointsIndex.IndexOf(sharedType);
    totalPriority += priority;

    Console.WriteLine($"{compartment1} - {compartment2} == {sharedType} / {priority}");
}

Console.WriteLine($"Total priority: {totalPriority}");