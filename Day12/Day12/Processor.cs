using System.Drawing;

namespace Day12
{
    public class Processor
    {
        public readonly string FileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day12\Day12\data.txt";
        public readonly string HeightIndex = @"abcdefghijklmnopqrstuvwxyz";
        public Node[,]? Grid;
        public int GridWidth = 0;
        public int GridHeight = 0;

        public int EndX = 0;
        public int EndY = 0;

        public Queue<Point> StartingPoints = new();

        public bool PathUpdated = true;

        public void Run()
        {
            LoadGrid();

            int minSteps = int.MaxValue;
            while (StartingPoints.Any())
            {
                var start = StartingPoints.Dequeue();
                ResetGrid(start);

                int iteration = 0;
                PathUpdated = true;
                while (PathUpdated)
                {
                    WalkGrid();
                    iteration++;
                }

                int stepsFromStart = Grid[EndX, EndY].StepsFromStart;
                Console.WriteLine($"\nNumber of iterations: {iteration}");
                Console.WriteLine($"Steps to end point: {stepsFromStart} from {start.X}, {start.Y}");

                if (stepsFromStart < minSteps) 
                    minSteps = stepsFromStart;
            }

            Console.WriteLine($"\nMinimum path length: {minSteps}");
        }

        private void ResetGrid(Point start)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    Grid[x, y].StepsFromStart = int.MaxValue;
                }
            }

            Grid[start.X, start.Y].StepsFromStart = 0;
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

                    if (ch is 'S' or 'a')
                    {
                        StartingPoints.Enqueue(new Point(x, y));
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
