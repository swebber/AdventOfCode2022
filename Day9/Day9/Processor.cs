using System.Drawing;

namespace Day9;

public class Processor
{
    private string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day9\Day9\data.txt";
    private readonly HeadLocation _headLocation = new();
    private readonly TailLocation _tailLocation = new();

    public void Run()
    {
        Console.WriteLine($"Head X: {_headLocation.X}, Y: {_headLocation.Y}");
        foreach (string line in File.ReadLines(_fileName))
        {
            if (line.Split(' ') is not [var direction, var distance])
                throw new ArgumentOutOfRangeException(nameof(line));
            MoveHead(direction, int.Parse(distance));
            Console.WriteLine($"Move: {line}, Head ({_headLocation.X},{_headLocation.Y}), Tail ({_tailLocation.X},{_tailLocation.Y})");
        }

        Console.WriteLine($"\nNumber of Tail positions: {_tailLocation.NumberOfTailLocations}");
    }

    private void MoveHead(string direction, int distance)
    {
        for (int i = 0; i < distance; i++)
        {
            _headLocation.Move(direction);
            _tailLocation.Follow(_headLocation);
        }
    }
}

public class TailLocation
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public List<Point> Points { get; set; } = new();
    public int NumberOfTailLocations => Points.Count;

    public TailLocation()
    {
        Points.Add(new Point(X, Y));
    }

    public void Follow(HeadLocation head)
    {
        double a = Math.Pow(head.X - X, 2d);
        double b = Math.Pow(head.Y - Y, 2d);
        double c = Math.Sqrt(a + b);

        if (c < 2d) return;

        int rowOffset = 0;
        if (head.X > X) rowOffset = 1;
        else if (head.X == X) rowOffset = 0;
        else if (head.X < X) rowOffset = -1;

        int colOffset = 0;
        if (head.Y > Y) colOffset = 1;
        else if (head.Y == Y) colOffset = 0;
        else if (head.Y < Y) colOffset = -1;

        X += rowOffset;
        Y += colOffset;

        var pointAfterMove = new Point(X, Y);
        if (!Points.Contains(pointAfterMove)) Points.Add(pointAfterMove);
    }
}

public class HeadLocation
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public void Move(string direction)
    {
        switch (direction)
        {
            case "U":
                Y++;
                break;
            case "L":
                X--;
                break;
            case "R":
                X++;
                break;
            case "D":
                Y--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        }
    }
}