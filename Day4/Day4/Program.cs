int contain = 0;

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day4\Day4\data.txt"))
{
    var (pair1, pair2) = Pairs(line);
    if (Overlaps(pair1, pair2))
    {
        Console.WriteLine($"Contained pair: {pair1} - {pair2}");
        contain++;
    }
}

Console.WriteLine($"Number of contained pairs: {contain}");

public static partial class Program
{
    public static (Tuple<int, int> pair1, Tuple<int, int> pair2) Pairs(string line)
    {
        var range = line.Split(",");

        var pair = range[0].Split("-");
        var pair1 = new Tuple<int, int>(int.Parse(pair[0]), int.Parse(pair[1]));

        pair = range[1].Split("-");
        var pair2 = new Tuple<int, int>(int.Parse(pair[0]), int.Parse(pair[1]));

        return (pair1, pair2);
    }

    public static bool Overlaps(Tuple<int, int> p1, Tuple<int, int> p2)
    {
        if (p1.Item1 >= p2.Item1 && p1.Item1 <= p2.Item2) return true;
        if (p1.Item2 >= p2.Item1 && p1.Item2 <= p2.Item2) return true;
        if (p1.Item1 <= p2.Item1 && p1.Item2 >= p2.Item2) return true;
        return false;
    }

    public static bool EitherContains(Tuple<int, int> p1, Tuple<int, int> p2)
    {
        if (p1.Item1 <= p2.Item1 && p1.Item2 >= p2.Item2) return true;
        if (p2.Item1 <= p1.Item1 && p2.Item2 >= p1.Item2) return true;
        return false;
    }
}