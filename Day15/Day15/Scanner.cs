using System.Drawing;

namespace Day15;

public class Scanner
{
    public int MinX => _location.X - int.Abs(_nearestBeacon.X);
    public int MaxX => _location.X + int.Abs(_nearestBeacon.X);
    public int MinY => _location.Y - int.Abs(_nearestBeacon.Y);
    public int MaxY => _location.Y + int.Abs(_nearestBeacon.Y);

    private readonly Point _location;
    private readonly Point _nearestBeacon;

    public Scanner(int sx, int sy, int bx, int by)
    {
        _location.X = sx;
        _location.Y = sy;
        _nearestBeacon.X = bx;
        _nearestBeacon.Y = by;
    }
}