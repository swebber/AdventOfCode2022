using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Processor
    {
        public readonly string FileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day12\Day12\data.txt";
        public readonly string HeightIndex = @"abcdefghijklmnopqrstuvwxyz";
        public Node[,]? Grid;
        public int GridWidth = 0;
        public int GridHeight = 0;

        public int StartX = 0;
        public int StartY = 0;
        public int EndX = 0;
        public int EndY = 0;

        public bool PathUpdated = true;

        public void Run()
        {
            LoadGrid();

            int iteration = 0;
            while (PathUpdated)
            {
                WalkGrid();
                iteration++;
            }

            Console.WriteLine($"Number of iterations: {iteration}");
            Console.WriteLine($"Steps to end point: {Grid[EndX, EndY].StepsFromStart}");
        }

        private void WalkGrid()
        {
            PathUpdated = false;
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    StepFrom(x, y);
                }
            }
        }

        private void StepFrom(int startX, int startY)
        {
            if (Grid[startX, startY].StepsFromStart == int.MaxValue) return;

            int nextHeight = Grid[startX, startY].Height + 1;
            int nextStep = Grid[startX, startY].StepsFromStart + 1;

            // up
            int x = startX;
            int y = startY - 1;

            if (y >= 0 && Grid[x,y].Height <= nextHeight && Grid[x,y].StepsFromStart > nextStep)
            {
                Grid[x, y].StepsFromStart = nextStep;
                PathUpdated = true;
            }

            // down
            x = startX;
            y = startY + 1;

            if (y < GridHeight && Grid[x, y].Height <= nextHeight && Grid[x, y].StepsFromStart > nextStep)
            {
                Grid[x, y].StepsFromStart = nextStep;
                PathUpdated = true;
            }

            // left
            x = startX - 1;
            y = startY;

            if (x >= 0 && Grid[x, y].Height <= nextHeight && Grid[x, y].StepsFromStart > nextStep)
            {
                Grid[x, y].StepsFromStart = nextStep;
                PathUpdated = true;
            }

            // right
            x = startX + 1;
            y = startY;

            if (x < GridWidth && Grid[x, y].Height <= nextHeight && Grid[x, y].StepsFromStart > nextStep)
            {
                Grid[x, y].StepsFromStart = nextStep;
                PathUpdated = true;
            }
        }

        private void LoadGrid()
        {
            bool firstPass = true;
            GridHeight = File.ReadLines(FileName).Count();

            int x = 0;
            int y = 0;

            foreach (var line in File.ReadLines(FileName))
            {
                if (firstPass)
                {
                    GridWidth = line.Length;
                    Grid = new Node[GridWidth, GridHeight];
                    firstPass = false;
                }

                foreach (char ch in line)
                {
                    bool isStart = false;
                    bool isEnd = false;
                    int height = 0;

                    switch (ch)
                    {
                        case 'S':
                            isStart = true;
                            StartX = x;
                            StartY = y;
                            break;
                        case 'E':
                            isEnd = true;
                            height = 26;
                            EndX = x;
                            EndY = y;
                            break;
                        default:
                            height = HeightIndex.IndexOf(ch) + 1;
                            break;
                    }

                    Grid[x, y] = new Node(height, isStart, isEnd);
                    if (isStart) Grid[x, y].StepsFromStart = 0;
                    x++;
                }

                x = 0;
                ++y;
            }
        }
    }

    public class Node
    {
        public int Height { get; }
        public bool IsStart { get; }
        public bool IsEnd { get; }
        public int StepsFromStart { get; set; } = int.MaxValue;

        public Node(int height, bool isStart, bool isEnd)
        {
            Height = height;
            IsStart = isStart;
            IsEnd = isEnd;
        }
    }
}
