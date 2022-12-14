using System.Linq.Expressions;

namespace Day13
{
    public class Processor
    {
        private readonly string FileName = @"C:\Users\WebberS\source\repos\AdventOfCode2022\Day13\Day13\data.txt";

        private enum Status
        {
            Undefined,
            InOrder,
            OutOfOrder
        }

        public void Run()
        {
            Queue<string>? packet1 = null;
            Queue<string>? packet2 = null;

            int sum = 0;
            int packetIndex = 0;

            foreach (var line in File.ReadLines(FileName))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string packet = RemoveOutsideBrackets(line);

                if (packet1 == null)
                {
                    packet1 = ParsePacket(packet);
                    continue;
                }
                else if (packet2 == null)
                {
                    packet2 = ParsePacket(packet);
                };

                var status = ComparePackets(packet1, packet2);

                packetIndex++;
                if (status == Status.InOrder) sum += packetIndex;

                packet1 = null;
                packet2 = null;
            }

            Console.WriteLine($"Sum of indices is: {sum}");
        }

        private Status ComparePackets(Queue<string> packet1, Queue<string> packet2)
        {
            while (true)
            {
                string? item1 = packet1.Any() ? packet1.Dequeue() : null;
                string? item2 = packet2.Any() ? packet2.Dequeue() : null;

                if (item1 == null && item2 == null) return Status.Undefined;
                if (item1 == null && item2 != null) return Status.InOrder;
                if (item1 != null && item2 == null) return Status.OutOfOrder;

                if (int.TryParse(item1, out int value1) && int.TryParse(item2, out int value2))
                {
                    if (value1 < value2) return Status.InOrder;
                    if (value1 > value2) return Status.OutOfOrder;
                    continue;
                }

                if (item1.StartsWith('[')) item1 = RemoveOutsideBrackets(item1);
                if (item2.StartsWith('[')) item2 = RemoveOutsideBrackets(item2);

                var q1 = ParsePacket(item1);
                var q2 = ParsePacket(item2);

                var status = ComparePackets(q1, q2);
                if (status is Status.InOrder or Status.OutOfOrder) return status;
            }
        }

        private string RemoveOutsideBrackets(string value)
        {
            if (value.Length == 2) return "";
            if (value.Length == 3) return value[1].ToString();
            return value.Substring(1, value.Length - 2);
        }

        private Queue<string> ParsePacket(string packet)
        {
            string item = "";
            Queue<string> items = new();

            bool inArray = false;
            int bracketCount = 0;

            foreach (var ch in packet)
            {
                if (!inArray && ch is >= '0' and <= '9')
                {
                    item += ch;
                }
                else if (!inArray && ch == ',')
                {
                    if (item.Length <= 0) continue;
                    items.Enqueue(item);
                    item = "";
                }
                else if (!inArray && ch == '[')
                {
                    inArray = true;
                    bracketCount++;
                    item += ch;
                }
                else if (inArray && ch == '[')
                {
                    bracketCount++;
                    item += ch;
                }
                else if (inArray && (ch is >= '0' and <= '9' || ch == ','))
                {
                    item += ch;
                }
                else if (inArray && ch == ']')
                {
                    bracketCount--;
                    item += ch;
                    if (bracketCount != 0) continue;
                    inArray = false;
                    items.Enqueue(item);
                    item = "";
                }
            }

            if (item.Length > 0)
            {
                items.Enqueue(item);
            }

            return items;
        }

        private static void DumpQueue(Queue<string> items)
        {
            bool firstPass = true;
            foreach (var foo in items)
            {
                if (firstPass)
                {
                    firstPass = false;
                    Console.Write($"\n{foo}");
                }
                else
                {
                    Console.Write($" | {foo}");
                }
            }

            Console.WriteLine();
        }
    }
}
