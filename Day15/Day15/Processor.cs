using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Day15
{
    public class Processor
    {
        private readonly string _fileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day15\Day15\test-data.txt";

        private readonly List<Scanner> _scanners = new();
        private Point _minPoint = new(int.MaxValue, int.MaxValue);
        private Point _maxPoint = new(int.MinValue, int.MinValue);

        public void Run()
        {
            LoadScanners();
            SizeGrid();
        }

        private void SizeGrid()
        {
            foreach (var scanner in _scanners)
            {
                if (scanner.MinX < _minPoint.X) _minPoint.X = scanner.MinX;
                if (scanner.MaxX > _maxPoint.X) _maxPoint.X = scanner.MaxX;
                if (scanner.MinY < _minPoint.Y) _minPoint.Y = scanner.MinY;
                if (scanner.MaxY > _maxPoint.Y) _maxPoint.Y = scanner.MaxY;
            }
        }

        private void LoadScanners()
        {
            foreach (string line in File.ReadLines(_fileName))
            {
                string adjusted = line.Trim();
                adjusted = adjusted.Replace(",", "").Replace(":", "");
                if (adjusted.Split(' ') is [_, _, var xPart, var yPart, .., var xDist, var yDist])
                {
                    int sx = int.Parse(xPart.Replace("x=", ""));
                    int sy = int.Parse(yPart.Replace("y=", ""));
                    int bx = int.Parse(xDist.Replace("x=", ""));
                    int by = int.Parse(yDist.Replace("y=", ""));

                    _scanners.Add(new(sx, sy, bx, by));
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(line));
                }
            }
        }
    }
}
