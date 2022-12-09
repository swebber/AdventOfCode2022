namespace Day5
{
    internal class Processor
    {
        readonly List<Stack<char>> _stacks = new();
        public int StackCount { get; set; }

        public void Run()
        {
            bool firstLine = true;
            bool loadingStacks = true;
            bool movingItems = false;

            foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day5\Day5\data.txt"))
            {
                if (firstLine)
                {
                    StackCount = ((line.Length - 3) / 4) + 1;
                    for (int i = 0; i < StackCount; i++)
                    {
                        _stacks.Add(new Stack<char>());
                    }

                    firstLine = false;
                }

                if (string.IsNullOrEmpty(line))
                {
                    loadingStacks = false;
                    movingItems = true;

                    ReverseStacks();

                    continue;
                }

                if (loadingStacks) AddToStacks(line);
                if (movingItems) MoveItems(line);
            }

            string answer = string.Empty;
            foreach (var stack in _stacks)
            {
                answer += stack.Pop();
            }

            Console.WriteLine(answer);
        }

        public void ReverseStacks()
        {
            Queue<char> queue = new();

            for (int i = 0; i < StackCount; i++)
            {
                while (_stacks[i].Count > 0)
                {
                    char item = _stacks[i].Pop();
                    queue.Enqueue(item);
                }

                while (queue.Count > 0)
                {
                    char item = queue.Dequeue();
                    _stacks[i].Push(item);
                }
            }
        }

        public void MoveItems(string line)
        {
            var commands = line.Split(' ');

            int itemsToMove = int.Parse(commands[1]);
            int fromStack = int.Parse(commands[3]);
            int toStack = int.Parse(commands[5]);

            for (int i = 0; i < itemsToMove; i++)
            {
                var item = _stacks[fromStack - 1].Pop();
                _stacks[toStack - 1].Push(item);
            }
        }

        public void AddToStacks(string line)
        {
            if (line.Contains('1')) return;

            int pos = -3;
            const int offset = 4;

            for (int i = 0; i < StackCount; i++)
            {
                pos += offset;
                char item = line[pos];
                if (item != ' ') _stacks[i].Push(item);
            }
        }
    }
}
