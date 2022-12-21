using System.Drawing;

namespace Day15;

public class Scanner
{
    public int MinX => _location.X - _offset;
    public int MaxX => _location.X + _offset;
    public int MinY => _location.Y - _offset;
    public int MaxY => _location.Y + _offset;
    public string Location => $"({_location.X},{_location.Y})";
    public string Beacon => $"({_nearestBeacon.X},{_nearestBeacon.Y})";

    private readonly Point _location;
    private readonly Point _nearestBeacon;
    private readonly int _offset;

    public bool Contains(int x, int y)
    {
        if (x < MinX || x > MaxX) return false;
        if (y < MinY || y > MaxY) return false;

        int xOffset = int.Abs(_location.X - x);
        int yOffset = int.Abs(_location.Y - y);
        int offset = xOffset + yOffset;

        return offset <= _offset;
    }

    public Scanner(int sx, int sy, int bx, int by)
    {
        _location.X = sx;
        _location.Y = sy;
        _nearestBeacon.X = bx;
        _nearestBeacon.Y = by;

        int xOffset = int.Abs(_location.X - _nearestBeacon.X);
        int yOffset = int.Abs(_location.Y - _nearestBeacon.Y);
        _offset = xOffset + yOffset;
    }
}