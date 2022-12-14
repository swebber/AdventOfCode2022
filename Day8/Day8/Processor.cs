namespace Day8
{
    public class Processor
    {
        private const string FileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day8\Day8\data.txt";

        private int _maxRows;
        private int _maxCols;
        private int[,] _treeGrid;

        public void Run()
        {
            LoadTreeGrid();

            //DumpTree();

            //int outerEdgeCount = OuterEdgeCount();
            //int visibleTreeCount = VisibleTreeCount();
            //Console.WriteLine($"Number of trees visible from outside the grid: {outerEdgeCount + visibleTreeCount}");

            int maxScenicScore = 0;
            int scenicRow = 0;
            int scenicCol = 0;

            for (int row = 0; row < _maxRows; row++)
            {
                for (int col = 0; col < _maxCols; col++)
                {
                    int scenicScore = ScenicScore(row, col);
                    if (scenicScore > maxScenicScore)
                    {
                        maxScenicScore = scenicScore;
                        scenicRow = row;
                        scenicCol = col;
                    }
                }
            }

            Console.WriteLine($"Row: {scenicRow}, Col: {scenicCol}, Score: {maxScenicScore}");
        }

        private int ScenicScore(int row, int col)
        {
            int top = ScenicScoreFromTop(row, col);
            int left = ScenicScoreFromLeft(row, col);
            int right = ScenicScoreFromRight(row, col);
            int bottom = ScenicScoreFromBottom(row, col);

            return top * right * bottom * left;
        }

        private int ScenicScoreFromLeft(int row, int col)
        {
            if (col == 0) return 0;

            int score = 0;
            int maxHeight = _treeGrid[row, col];
            while (--col >= 0)
            {
                score++;
                int nextHeight = _treeGrid[row, col];
                if (nextHeight >= maxHeight) break;
            }

            return score;
        }

        private int ScenicScoreFromBottom(int row, int col)
        {
            if (row == _maxRows - 1) return 0;

            int score = 0;
            int maxHeight = _treeGrid[row, col];
            while (++row < _maxRows)
            {
                ++score;
                int nextHeight = _treeGrid[row, col];
                if (nextHeight >= maxHeight) break;
            }

            return score;
        }

        private int ScenicScoreFromRight(int row, int col)
        {
            if (col == _maxCols - 1) return 0;

            int score = 0;
            int maxHeight = _treeGrid[row, col];
            while (++col < _maxCols)
            {
                ++score;
                int nextHeight = _treeGrid[row, col];
                if (nextHeight >= maxHeight) break;
            }

            return score;
        }

        private int ScenicScoreFromTop(int row, int col)
        {
            if (row == 0) return 0;

            int score = 0;
            int maxHeight = _treeGrid[row, col];
            while (--row >= 0)
            {
                ++score;
                int nextHeight = _treeGrid[row, col];
                if (nextHeight >= maxHeight) break;
            }

            return score;
        }

        private int VisibleTreeCount()
        {
            int visibleTreeCount = 0;
            int rows = _maxRows - 1;
            int cols = _maxCols - 1;
            for (int row = 1; row < rows; row++)
            {
                for (int col = 1; col < cols; col++)
                {
                    if (TreeIsVisible(row, col)) ++visibleTreeCount;
                }
            }

            return visibleTreeCount;
        }

        private bool TreeIsVisible(int row, int col)
        {
            if (TreeIsVisibleFromTop(row, col)) return true;
            if (TreeIsVisibleFromRight(row, col)) return true;
            if (TreeIsVisibleFromBottom(row, col)) return true;
            if (TreeIsVisibleFromLeft(row, col)) return true;
            return false;
        }

        private bool TreeIsVisibleFromLeft(int row, int col)
        {
            int treeHeight = _treeGrid[row, col];
            while (--col >= 0)
            {
                if (_treeGrid[row, col] >= treeHeight)
                    return false;
            }

            return true;
        }

        private bool TreeIsVisibleFromBottom(int row, int col)
        {
            int treeHeight = _treeGrid[row, col];
            while (++row < _maxRows)
            {
                if (_treeGrid[row, col] >= treeHeight)
                    return false;
            }

            return true;
        }

        private bool TreeIsVisibleFromRight(int row, int col)
        {
            int treeHeight = _treeGrid[row, col];
            while (++col < _maxCols)
            {
                if (_treeGrid[row, col] >= treeHeight)
                    return false;
            }
            return true;
        }

        private bool TreeIsVisibleFromTop(int row, int col)
        {
            int treeHeight = _treeGrid[row, col];
            while (--row >= 0)
            {
                if (_treeGrid[row, col] >= treeHeight)
                    return false;
            }

            return true;
        }

        private int OuterEdgeCount()
        {
            return ((_maxRows - 1) * 2) + ((_maxCols - 1) * 2);
        }

        private void DumpTree()
        {
            int rows = _treeGrid.GetLength(0);
            int cols = _treeGrid.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Console.Write($"{_treeGrid[row, col]}");
                }

                Console.WriteLine();
            }
        }

        private void LoadTreeGrid()
        {
            bool firstPass = true;
            int height = File.ReadLines(FileName).Count();

            int row = 0;
            foreach (string line in File.ReadLines(FileName))
            {
                if (firstPass)
                {
                    int width = line.Length;
                    _treeGrid = new int[width, height];
                    _maxRows = width;
                    _maxCols = height;
                    firstPass = false;
                }

                int col = 0;
                foreach (var ch in line)
                {
                    _treeGrid[row, col] = int.Parse(ch.ToString());
                    ++col;
                }

                ++row;
            }
        }
    }
}
