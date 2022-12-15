using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class Processor
    {
        private enum Movement
        {
            Undefined,
            Blocked,
            Success,
            HasReachedTheAbyss
        }

        private readonly string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day14\Day14\data.txt";

        private readonly int _loopMax = 100;

        private int _xMin = int.MaxValue;
        private int _xMax = int.MinValue;
        private int _yMin = int.MaxValue;
        private int _yMax = int.MinValue;

        private List<Point> _map = new();

        public void Run()
        {
            BuildMap();

            var sand = new Point(500, 0);

            int count = 0;

            while (true)
            {
                var state = Move(sand);
                if (state == Movement.HasReachedTheAbyss) break;

                if (++count % 100 == 0) Console.WriteLine($"Units of sand: {count}");
            }

            Console.WriteLine($"Last unit of sand before reaching the abyss: {count}");
        }

        private Movement Move(Point sand)
        {
            if (HasReachedTheAbyss(sand)) return Movement.HasReachedTheAbyss;

            while (true)
            {
                var state = MoveDown(ref sand);
                if (state == Movement.HasReachedTheAbyss) return Movement.HasReachedTheAbyss;
                if (state == Movement.Success) continue;

                state = MoveLeft(ref sand);
                if (state == Movement.HasReachedTheAbyss) return Movement.HasReachedTheAbyss;
                if (state == Movement.Success) continue;

                state = MoveRight(ref sand);
                if (state == Movement.HasReachedTheAbyss) return Movement.HasReachedTheAbyss;
                if (state == Movement.Success) continue;

                _map.Add(sand);
                return Movement.Blocked;
            }
        }

        private Movement MoveRight(ref Point sand)
        {
            sand.X += 1;
            sand.Y += 1;
            if (HasReachedTheAbyss(sand)) return Movement.HasReachedTheAbyss;

            if (_map.Contains(sand))
            {
                sand.X -= 1;
                sand.Y -= 1;
                return Movement.Blocked;
            }

            return Movement.Success;
        }

        private Movement MoveLeft(ref Point sand)
        {
            sand.X -= 1;
            sand.Y += 1;
            if (HasReachedTheAbyss(sand)) return Movement.HasReachedTheAbyss;

            if (_map.Contains(sand))
            {
                sand.X += 1;
                sand.Y -= 1;
                return Movement.Blocked;
            }

            return Movement.Success;
        }

        private Movement MoveDown(ref Point sand)
        {
            sand.Y += 1;
            if (HasReachedTheAbyss(sand)) return Movement.HasReachedTheAbyss;

            if (_map.Contains(sand))
            {
                sand.Y -= 1;
                return Movement.Blocked;
            }

            return Movement.Success;
        }

        private bool HasReachedTheAbyss(Point sand)
        {
            return sand.Y > _yMax || sand.X < _xMin || sand.X > _xMax;
        }

        private void BuildMap()
        {
            foreach (var line in File.ReadLines(_fileName))
            {
                var wallCorners = QueueWallCorners(line);

                bool firstCorner = true;
                Point previousCorner = new(0, 0);

                while (wallCorners.Any())
                {
                    var corner = wallCorners.Dequeue();

                    if (firstCorner)
                    {
                        _map.Add(corner);
                        previousCorner = corner;
                        firstCorner = false;
                        continue;
                    }

                    Add(corner);

                    int xOffset = 0;
                    if (corner.X > previousCorner.X) xOffset = 1;
                    else if (corner.X < previousCorner.X) xOffset = -1;

                    int yOffset = 0;
                    if (corner.Y > previousCorner.Y) yOffset = 1;
                    else if (corner.Y < previousCorner.Y) yOffset = -1;

                    Point edge = previousCorner;

                    int index = 0;
                    while (true)
                    {
                        edge = new(edge.X + xOffset, edge.Y + yOffset);
                        if (edge == corner) break;
                        Add(edge);
                        if (++index > _loopMax) throw new Exception();
                    }

                    previousCorner = corner;;
                }
            }
        }

        private void Add(Point p)
        {
            if (!_map.Contains(p)) _map.Add(p);
        }

        private Queue<Point> QueueWallCorners(string line)
        {
            Queue<Point> corners = new();

            var coordinates = line.Split(" -> ");
            foreach (var coordinate in coordinates)
            {
                if (coordinate.Split(',') is not [var x, var y])
                {
                    throw new ArgumentOutOfRangeException(nameof(line));
                }

                if (int.TryParse(x, out int xResult))
                {
                    if (xResult < _xMin) _xMin = xResult;
                    if (xResult > _xMax) _xMax = xResult;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(line));
                }

                if (int.TryParse(y, out int yResult))
                {
                    if (yResult < _yMin) _yMin = yResult;
                    if (yResult > _yMax) _yMax = yResult;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(line));
                }

                corners.Enqueue(new Point(xResult, yResult));
            }

            return corners;
        }
    }
}
    