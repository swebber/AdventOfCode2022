string pointsIndex = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
int totalPriority = 0;

List<string> elfGroup = new();

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day3\Day3\data.txt"))
{
    elfGroup.Add(line);
    if (elfGroup.Count < 3) continue;

    var foo = elfGroup[0].Intersect(elfGroup[1]);
    var bar = elfGroup[1].Intersect(elfGroup[2]);
    var sharedType = foo.Intersect(bar).First();
    
    int priority = pointsIndex.IndexOf(sharedType);
    totalPriority += priority;

    Console.WriteLine($"{elfGroup[0]}\n{elfGroup[1]}\n{elfGroup[2]}\nShared item: {sharedType}, Priority: {priority}\n");

    elfGroup = new List<string>();
}

Console.WriteLine($"Total priority: {totalPriority}");