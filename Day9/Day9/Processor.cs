using System.Drawing;

namespace Day9;

public class Processor
{
    private string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day9\Day9\data.txt";

    private const int NumberOfKnots = 10;
    private readonly int _numberOfTails = NumberOfKnots - 1;

    private readonly HeadLocation _headLocation = new();
    private readonly List<TailLocation> _tailLocations = new();

    public Processor()
    {
        int numberOfTails = NumberOfKnots - 1;

        for (int i = 0; i < numberOfTails; i++)
        {
            bool trackPosition = i + 1 == _numberOfTails;
            _tailLocations.Add(new TailLocation((i + 1).ToString(), trackPosition));
        }
    }

    public void Run()
    {
        Console.WriteLine($"Head X: {_headLocation.X}, Y: {_headLocation.Y}");
        foreach (string line in File.ReadLines(_fileName))
        {
            if (line.Split(' ') is not [var direction, var distance])
                throw new ArgumentOutOfRangeException(nameof(line));
            MoveHead(direction, int.Parse(distance));

            Console.Write($"Move: {line}, Head ({_headLocation.X},{_headLocation.Y})");
            foreach (var knot in _tailLocations)
            {
                Console.Write($", Tail {knot.Name} ({knot.X},{knot.Y})");
            }
            Console.WriteLine();
        }

        TailLocation lastKnot = _tailLocations.Last();
        Console.WriteLine($"\nNumber of Tail positions: {lastKnot.NumberOfTailLocations}");
    }

    private void MoveHead(string direction, int distance)
    {
        for (int i = 0; i < distance; i++)
        {
            _headLocation.Move(direction);

            ILocation previousKnot  = _headLocation;
            foreach (var tailLocation in _tailLocations)
            {
                tailLocation.Follow(previousKnot);
                previousKnot = tailLocation;
            }
        }
    }
}

public class TailLocation : ILocation
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public string Name { get; }
    private readonly bool _trackPosition;
    
    public List<Point> Points { get; set; } = new();
    public int NumberOfTailLocations => Points.Count;

    public TailLocation(string name, bool trackPosition = false)
    {
        Name = name;
        _trackPosition = trackPosition;
        if (_trackPosition)
            Points.Add(new Point(X, Y));
    }

    public void Follow(ILocation head)
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

        if (_trackPosition)
        {
            var pointAfterMove = new Point(X, Y);
            if (!Points.Contains(pointAfterMove)) 
                Points.Add(pointAfterMove);
        }
    }
}

public class HeadLocation : ILocation
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

public interface ILocation
{
    public int X { get; set; }
    public int Y { get; set; }
}