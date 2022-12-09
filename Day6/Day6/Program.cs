var line = File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day6\Day6\data.txt").First();

const int packetLength = 4;
const int packetOffset = 3;
int packetCount = line.Length - packetOffset;

for (int i = 0; i < packetCount; i++)
{
    string packet = line.Substring(i, packetLength);
    if (packet.Distinct().Count() == packetLength)
    {
        Console.WriteLine($"First market after character: {i + packetLength}");
        return;
    }
}

Console.WriteLine("No marker found.");
