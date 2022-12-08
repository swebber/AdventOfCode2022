// a,x == rock     == 1
// b,y == paper    == 2
// c,z == scissors == 3
// lose == 0
// draw == 3
// win  == 6

int totalScore = 0;

foreach (string line in File.ReadLines(@"C:\Users\WebberS\source\repos\AdventOfCode2022\Day2\Day2\data.txt"))
{
    var play = line.Split(" ");
    int score = Value(play[1]);
    int points = Points(play);
    int total = score + points;

    Console.WriteLine($"{score} + {points} = {total}");

    totalScore += total;
}

Console.WriteLine($"Total: {totalScore}");

public static partial class Program
{
    public static int Points(string[] play)
    {
        string pair = $"{play[1]}{play[0]}";

        return pair switch
        {
            "XA" or "YB" or "ZC" => 3,
            "XC" or "YA" or "ZB" => 6,
            _ => 0
        };
    }

    public static int Value(string move)
    {
        switch (move)
        {
            case "A":
            case "X":
                return 1;
            case "B":
            case "Y":
                return 2;
            case "C":
            case "Z":
                return 3;
        }

        throw new ArgumentOutOfRangeException(nameof(move));
    }
}