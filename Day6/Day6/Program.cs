var line = File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day6\Day6\data.txt").First();

const int messageMarkerLength = 14;
int messageMarkerCount = line.Length - (messageMarkerLength - 1);

for (int i = 0; i < messageMarkerCount; i++)
{
    string packet = line.Substring(i, messageMarkerLength);
    if (packet.Distinct().Count() == messageMarkerLength)
    {
        Console.WriteLine($"First marker after character: {i + messageMarkerLength}");
        return;
    }
}

Console.WriteLine("No marker found.");
